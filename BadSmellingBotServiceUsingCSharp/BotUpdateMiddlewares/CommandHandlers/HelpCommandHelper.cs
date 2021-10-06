using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.CommandHandlers
{
    public class HelpCommandHelper : ICommandHandler
    {
        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message?.Chat.Id ?? -1;
            await botClient.SendTextMessageAsync(chatId, $"/start для приветствия\n" +
                                                         $"/help для получения списка команд",
                cancellationToken: cancellationToken);
        }
    }
}