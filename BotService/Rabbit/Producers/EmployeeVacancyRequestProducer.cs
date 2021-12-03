using System.Threading.Tasks;
using BotService.Models.Rabbit;
using CoreDTO.Redis.Vacation;
using RabbitMQ.Client;
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

        public void Produce(VacationRequestDto dto)
        {
            _rabbitMqService.Publish(dto, ExchangeType.Direct, RouteKeys.RequestService);
        }
    }
}