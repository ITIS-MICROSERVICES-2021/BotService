using System;
using System.Globalization;
using System.Threading.Tasks;
using BotService.Models.Rabbit;
using BotService.Rabbit.Producers;
using Telegram.Bot.Host.BotServer;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;

namespace BotService.NotCommandHandlers
{
    [BotCommand(CommandText = "EmployeeVacancyRequest")]
    public class EmployeeVacancyRequestHandler : ICommandHandler
    {
        private readonly EmployeeVacancyRequestProducer _producer;

        public EmployeeVacancyRequestHandler(EmployeeVacancyRequestProducer producer)
        {
            _producer = producer;
        }

        public async Task HandleAsync(BotUpdateContext botUpdateContext)
        {
            var message = botUpdateContext.Update.Message.Text;
            var chatId = botUpdateContext.Update.Message!.Chat.Id;
            var splitMessage = message.Split(" ");
            try
            {
                var dateFrom = DateTime.ParseExact(splitMessage[4], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                var dateTo = DateTime.ParseExact(splitMessage[6], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                _producer.Produce(new EmployeeVacancyRequest()
                    {From = dateFrom, To = dateTo, Username = botUpdateContext.Update.Message.From.Username});
                await botUpdateContext.BotClient.SendTextMessageAsync(chatId, "Заявка отправлена",
                    cancellationToken: botUpdateContext.CancellationToken);
            }
            catch (Exception _)
            {
                await botUpdateContext.BotClient.SendTextMessageAsync(chatId, "Введите дату в корректном формате",
                    cancellationToken: botUpdateContext.CancellationToken);
            }
        }
    }
}