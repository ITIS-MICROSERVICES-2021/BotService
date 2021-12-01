using BotService.Models;
using BotService.Models.Rabbit;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Services;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CoreDTO.Redis.Vacation;

namespace BotService.Rabbit.Consummer
{
    public class DirectorApprovalConsumer : IConsummer
    {
        public DirectorApprovalConsumer()
        {
        }

        public async Task Consume(BasicDeliverEventArgs args)
        {
            var str = Encoding.UTF8.GetString(args.Body.ToArray());
            var parsedObj = JsonConvert.DeserializeObject<VacationRequestDto>(str);
            // send telegram message
        }
    }
}
