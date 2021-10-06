using System;
using System.Collections.Generic;
using System.Linq;
using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares;
using Microsoft.Extensions.DependencyInjection;

namespace BadSmellingBotServiceUsingCSharp.Extensions
{
    public static class StartupMiddlewareExtensions
    {
        public static List<Type> AddBotUpdateMiddleware<TMiddleware>(this List<Type> prevMiddlewares)
            where TMiddleware : BotUpdateMiddleware
        {
            prevMiddlewares.Add(typeof(TMiddleware));
            return prevMiddlewares;
        }

        public static List<Type> AddStorage(this IServiceCollection services)
        {
            var prevMiddlewares = new List<Type>();

            services.AddScoped(provider =>
                new BotUpdateMiddlewareStorage(prevMiddlewares.Count == 0 
                    ? null 
                    : prevMiddlewares[0], provider));

            return prevMiddlewares;
        }

        public static void Confirm(this List<Type> prevMiddlewares, IServiceCollection services)
        {
            if (prevMiddlewares.Count <= 0) return; 
            
            for (var i = 0; i < prevMiddlewares.Count - 1; i++)
            {
                var type = prevMiddlewares[i];
                var nextType = prevMiddlewares[i + 1];
                services.AddScoped(type, provider => 
                    InstantiateMiddleware(type, ((BotUpdateMiddleware)provider.GetService(nextType))!.InvokeAsync));
            }

            var lastType = prevMiddlewares.Last();
            services.AddScoped(lastType, provider => InstantiateMiddleware(lastType, async (_, _, _) => {}));
        }

        private static BotUpdateMiddleware InstantiateMiddleware(Type type, BotUpdateMiddleware.BotUpdateDelegate nextDelegate)
        {
            var ctor = type.GetConstructor(new[] { typeof(BotUpdateMiddleware.BotUpdateDelegate) });
            var instance = ctor!.Invoke(new object[] { nextDelegate });
            return (BotUpdateMiddleware)instance;
        }
    }
}