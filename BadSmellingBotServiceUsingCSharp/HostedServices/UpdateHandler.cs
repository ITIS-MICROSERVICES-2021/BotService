using System.Threading;
using System.Threading.Tasks;
using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.HostedServices
{
    public class UpdateHandler
    {
        private readonly BotUpdateMiddlewareStorage _botUpdateMiddlewareStorage;

        public UpdateHandler(BotUpdateMiddlewareStorage botUpdateMiddlewareStorage)
        {
            _botUpdateMiddlewareStorage = botUpdateMiddlewareStorage;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            await _botUpdateMiddlewareStorage.RunMiddlewaresAsync(botClient, update, cancellationToken);
        }
    }
}