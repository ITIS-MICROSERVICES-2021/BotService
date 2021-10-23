using System.Linq;
using System.Threading.Tasks;
using BotService.Services;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;
using Telegram.Bot.Types.ReplyMarkups;

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
            var chatId = botUpdateContext.Update.Message?.Chat.Id ?? -1;

            var commands = _commandsListService.GetCommands();
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    commands.Select(x => new KeyboardButton(x)).ToArray()
                    //new KeyboardButton[] { "Three", "Four" },
                })
            {
                ResizeKeyboard = true
            };

            await botUpdateContext.BotClient.SendTextMessageAsync(chatId, _constantMessagesService.Greetings,
                cancellationToken: botUpdateContext.CancellationToken);

            await botUpdateContext.BotClient.SendTextMessageAsync(
                chatId,
                _constantMessagesService.AvailableCommands,
                replyMarkup: replyKeyboardMarkup, cancellationToken: botUpdateContext.CancellationToken);
        }
    }
}