using System.Threading.Tasks;
using BotService.Services;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;

namespace BotService.CommandHandlers
{
    [BotCommand(CommandText = "/help")]
    public class HelpCommandHandlers : ICommandHandler
    {
        private readonly ConstantMessagesService _constantMessagesService;

        public HelpCommandHandlers(ConstantMessagesService constantMessagesService)
        {
            _constantMessagesService = constantMessagesService;
        }

        public async Task HandleAsync(BotUpdateContext botUpdateContext)
        {
            var chatId = botUpdateContext.Update.Message?.Chat.Id ?? -1;
            await botUpdateContext.BotClient.SendTextMessageAsync(chatId, _constantMessagesService.Help,
                cancellationToken: botUpdateContext.CancellationToken);
        }
    }
}