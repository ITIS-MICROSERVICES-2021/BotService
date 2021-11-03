using System.Threading.Tasks;
using BotService.Services;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;

namespace BotService.CommandHandlers
{
    [BotCommand(CommandText = "/start")]
    public class StartCommandHandler : ICommandHandler
    {
        private readonly CommandsListService _commandsListService;
        private readonly ConstantMessagesService _constantMessagesService;

        public StartCommandHandler(CommandsListService commandsListService,
            ConstantMessagesService constantMessagesService)
        {
            _commandsListService = commandsListService;
            _constantMessagesService = constantMessagesService;
        }

        public async Task HandleAsync(BotUpdateContext botUpdateContext)
        {
            var chatId = botUpdateContext.Update.Message!.Chat.Id;
            await botUpdateContext.BotClient.SendTextMessageAsync(chatId, _constantMessagesService.Greetings,
                cancellationToken: botUpdateContext.CancellationToken);
        }
    }
}