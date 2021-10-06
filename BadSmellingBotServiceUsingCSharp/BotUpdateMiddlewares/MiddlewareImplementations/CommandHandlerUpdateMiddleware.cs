using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.CommandHandlers;
using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.HelperHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.MiddlewareImplementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CommandHandlerUpdateMiddleware : BotUpdateMiddleware
    {
        private readonly Dictionary<string, ICommandHandler> _commandHandlers;
        
        public CommandHandlerUpdateMiddleware(BotUpdateDelegate next) : base(next)
        {
            _commandHandlers = new Dictionary<string, ICommandHandler>
            {
                {"/start", new StartCommandHandler()},
                {"/help", new HelpCommandHelper()}
            };
        }

        public override async Task InvokeAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var command = update.Message?.Text?.ToLower();

            if (!_commandHandlers.ContainsKey(command!))
            {
                await CommandNotFoundHandler.CommandNotFound(botClient, update, cancellationToken);
                return;
            }

            await _commandHandlers[command].HandleAsync(botClient, update, cancellationToken);
            
            
            await Next(botClient, update, cancellationToken);
        }
    }
}