using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace BotService.Rabbit.Consummer
{
    public interface IConsummer
    {
        Task Consume(BasicDeliverEventArgs args);
    }
}
