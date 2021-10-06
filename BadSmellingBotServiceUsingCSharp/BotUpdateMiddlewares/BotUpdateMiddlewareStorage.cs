using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares
{
    public class BotUpdateMiddlewareStorage
    {
        private readonly Type _firstMiddleware;
        private readonly IServiceProvider _serviceProvider;

        public BotUpdateMiddlewareStorage(Type firstMiddleware, IServiceProvider serviceProvider)
        {
            _firstMiddleware = firstMiddleware;
            _serviceProvider = serviceProvider;
        }

        public async Task RunMiddlewaresAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            var instance = _serviceProvider.GetService(_firstMiddleware);
            var middleware = (BotUpdateMiddleware) instance;
            await middleware!.InvokeAsync(botClient, update, cancellationToken);
        }
        
    }
}