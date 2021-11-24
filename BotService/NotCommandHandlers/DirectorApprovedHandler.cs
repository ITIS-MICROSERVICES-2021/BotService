using System.Threading.Tasks;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;

namespace BotService.NotCommandHandlers
{
    [BotCommand(CommandText = "NotFoundHandler")]
    public class DirectorApprovedHandler : ICommandHandler
    {
        public async Task HandleAsync(BotUpdateContext botUpdateContext)
        {
            var chatId = botUpdateContext.Update.Message!.Chat.Id;
            await botUpdateContext.BotClient.SendTextMessageAsync(chatId, "Не удалось обработать сообщение",
                cancellationToken: botUpdateContext.CancellationToken);
        }
    }
}