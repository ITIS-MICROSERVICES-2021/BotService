using BotService.Models.Rabbit;
using BotService.Rabbit.Consummer;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotService.Rabbit
{
    public class RabbitConfig
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IConsummer _directorApprovalConsumer;

        public RabbitConfig(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
            _directorApprovalConsumer = new DirectorApprovalConsumer();
        }

        public void Init()
        {
            _rabbitMQService.Subscribe(async (BasicDeliverEventArgs args) => await _directorApprovalConsumer.Consume(args), "", RouteKeys.VacancyCreated, (ILogger)null); // TODO: ADD LOGGER
        }
    }
}
