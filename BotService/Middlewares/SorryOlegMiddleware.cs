using System.Threading.Tasks;
using Telegram.Bot.Host.Middleware;

namespace BotService.Middlewares
{
    public class SorryOlegMiddleware
    {
        private readonly BotUpdateDelegate _next;

        public SorryOlegMiddleware(BotUpdateDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(BotUpdateContext botUpdateContext)
        {
            var chatId = botUpdateContext.Update.Message?.Chat.Id ?? -1;

            if (botUpdateContext.Update.Message?.Chat.Username?.ToLower() is "aidahooleg")
            {
                await botUpdateContext.BotClient.SendTextMessageAsync(chatId,
                    "Извините, Олег, вам доступ к боту ограничен!\n" +
                    "Для разблокировки заплатите нам плюсик за выполненную работу:\n" +
                    "отлично сделанный телеграм бот на .NET, а именно на IronPython",
                    cancellationToken: botUpdateContext.CancellationToken);
                return;
            }

            await _next(botUpdateContext);
        }
    }
}