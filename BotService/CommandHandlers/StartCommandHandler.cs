using System;
using System.Threading.Tasks;
using BotService.Services;
using Microsoft.Extensions.Logging;
using RabbitMQ.Services;
using RedisIO.Services;
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
            ConstantMessagesService constantMessagesService, IRedisIOService redis, IRabbitMQService rabbitMqService, ILogger<StartCommandHandler> logger)
        {
            _commandsListService = commandsListService;
            _constantMessagesService = constantMessagesService;
            const string key = "myClass";
            var myClass = new MyClass() {Message = "Message"};
            redis.AddAsync(key, myClass);
            
            rabbitMqService.Subscribe<MyClass>(async n =>
            {
                var v = await redis.GetAsync<MyClass>(key);
                Console.WriteLine(v.Message);
            }, "exchange", key, logger);
            
            rabbitMqService.Publish(myClass, "exchange", key);
        }

        public async Task HandleAsync(BotUpdateContext botUpdateContext)
        {
            var chatId = botUpdateContext.Update.Message!.Chat.Id;
            await botUpdateContext.BotClient.SendTextMessageAsync(chatId, _constantMessagesService.Greetings,
                cancellationToken: botUpdateContext.CancellationToken);
        }
    }
}