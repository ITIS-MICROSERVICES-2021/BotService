
using BotService.Models;
using BotService.Models.Rabbit;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Services;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var parsedObj = JsonConvert.DeserializeObject<EmployeeVacancyRequest>(str);
            // send telegram message
        }
    }
}
