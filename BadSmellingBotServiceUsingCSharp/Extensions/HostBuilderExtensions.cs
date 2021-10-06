﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BadSmellingBotServiceUsingCSharp.Extensions
{
    public static class HostBuilderExtensions
    {
        private const string ConfigureServicesMethodName = "ConfigureServices";
        
        public static IHostBuilder UseStartup<TStartup>(
            this IHostBuilder hostBuilder) where TStartup : class
        {
            hostBuilder.ConfigureServices((ctx, serviceCollection) =>
            {
                var cfgServicesMethod = typeof(TStartup).GetMethod(
                    ConfigureServicesMethodName, new[] { typeof(IServiceCollection) });

                var hasConfigCtor = typeof(TStartup).GetConstructor(
                    new[] { typeof(IConfiguration) }) != null;

                var startUpObj = hasConfigCtor
                    ? (TStartup)Activator.CreateInstance(typeof(TStartup), ctx.Configuration)
                    : (TStartup)Activator.CreateInstance(typeof(TStartup), null);

                cfgServicesMethod?.Invoke(startUpObj, new object[] { serviceCollection });
            });

            return hostBuilder;
        }
    }
}