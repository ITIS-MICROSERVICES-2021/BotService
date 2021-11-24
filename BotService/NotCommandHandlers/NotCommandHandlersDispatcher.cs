using System.Threading.Tasks;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;

namespace BotService.NotCommandHandlers
{
    public class NotCommandHandlersDispatcher : ICommandNotFoundHandler
    {
        private readonly DirectorVacancyApproveHandler _directorVacancyApproveHandler;
        private readonly EmployeeVacancyRequestHandler _employeeVacancyRequestHandler;
        private readonly NotFoundHandler _notFoundHandler;

        public NotCommandHandlersDispatcher(DirectorVacancyApproveHandler directorVacancyApproveHandler,
            EmployeeVacancyRequestHandler employeeVacancyRequestHandler,
            NotFoundHandler notFoundHandler)
        {
            _directorVacancyApproveHandler = directorVacancyApproveHandler;
            _employeeVacancyRequestHandler = employeeVacancyRequestHandler;
            _notFoundHandler = notFoundHandler;
        }

        public async Task HandleAsync(BotUpdateContext botUpdateContext)
        {
            var message = botUpdateContext.Update.Message.Text;
            if (message.StartsWith("Хочу в отпуск"))
            {
                await _employeeVacancyRequestHandler.HandleAsync(botUpdateContext);
            }
            else if (message.StartsWith("Подтверждаю"))
            {
                await _directorVacancyApproveHandler.HandleAsync(botUpdateContext);
            }
            else
            {
                await _notFoundHandler.HandleAsync(botUpdateContext);
            }
        }
    }
}