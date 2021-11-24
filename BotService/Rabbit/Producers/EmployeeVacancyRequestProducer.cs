using System.Threading.Tasks;
using BotService.Models.Rabbit;
using RabbitMQ.Services;

namespace BotService.Rabbit.Producers
{
    public class EmployeeVacancyRequestProducer
    {
        private readonly IRabbitMQService _rabbitMqService;

        public EmployeeVacancyRequestProducer(IRabbitMQService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public void Produce(EmployeeVacancyRequest dto)
        {
            _rabbitMqService.Publish(dto, "", RouteKeys.VacancyCreated);
        }
    }
}