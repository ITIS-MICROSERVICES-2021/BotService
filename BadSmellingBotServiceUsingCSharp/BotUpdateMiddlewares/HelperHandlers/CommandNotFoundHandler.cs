using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.HelperHandlers
{
    public static class CommandNotFoundHandler
    {
        public static async Task CommandNotFound(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message?.Chat.Id ?? -1;
            await botClient.SendTextMessageAsync(chatId, $"Команда не найдена",
                cancellationToken: cancellationToken);
        }
    }
}