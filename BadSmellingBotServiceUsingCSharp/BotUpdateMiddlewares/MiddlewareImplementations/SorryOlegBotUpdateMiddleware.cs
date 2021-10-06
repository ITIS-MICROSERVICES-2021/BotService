using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.MiddlewareImplementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SorryOlegBotUpdateMiddleware : BotUpdateMiddleware
    {
        public SorryOlegBotUpdateMiddleware(BotUpdateDelegate next) : base(next)
        {
        }

        public override async Task InvokeAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message?.Chat.Id ?? -1;

            if (update.Message?.Chat.Username?.ToLower() is "aidahooleg")
            {
                await botClient.SendTextMessageAsync(chatId, $"Извините, Олег, вам доступ к боту ограничен!\n" +
                                                             $"Для разблокировки заплатите нам плюсик за выполненную работу:\n" +
                                                             $"отлично сделанный телеграм бот на .NET, а именно на IronPython",
                    cancellationToken: cancellationToken);
                return;
            }

            await Next(botClient, update, cancellationToken);
        }
    }
}