using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares
{
    public abstract class BotUpdateMiddleware
    {
        public delegate Task BotUpdateDelegate(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken);

        protected readonly BotUpdateDelegate Next;

        protected BotUpdateMiddleware(BotUpdateDelegate next)
        {
            Next = next;
        }

        public abstract Task InvokeAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken);
    }
}