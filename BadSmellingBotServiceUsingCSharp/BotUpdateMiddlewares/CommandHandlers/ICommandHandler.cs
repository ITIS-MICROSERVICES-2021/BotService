using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.CommandHandlers
{
    public interface ICommandHandler
    {
        public Task HandleAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken);
    }
}