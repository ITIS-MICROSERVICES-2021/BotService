using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.CommandHandlers
{
    public class StartCommandHandler : ICommandHandler
    {
        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message?.Chat.Id ?? -1;
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[] { "/start", "/help" },
                    //new KeyboardButton[] { "Three", "Four" },
                })
            {
                ResizeKeyboard = true
            };

            await botClient.SendTextMessageAsync(chatId, $"Приветствуем вас в боте",
                cancellationToken: cancellationToken);
            
            await botClient.SendTextMessageAsync(
                chatId,
                "Доступные команды",
                replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);
        }
    }
}