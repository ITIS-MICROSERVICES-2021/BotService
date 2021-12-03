using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BotService.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Services;
using RedisIO.Services;
using Telegram.Bot.Host.Middleware;

namespace BotService.Middlewares
{
    public class SaveUsersMiddleware
    {
        private readonly BotUpdateDelegate _next;

        public SaveUsersMiddleware(BotUpdateDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(BotUpdateContext botUpdateContext)
        {
            var userChatData = new ChatUserData
            {
                Id = botUpdateContext.Update.Message?.Chat.Id ?? -1,
                Username = botUpdateContext.Update.Message?.Chat.Username,
                FirstName = botUpdateContext.Update.Message?.Chat.FirstName,
                LastName = botUpdateContext.Update.Message?.Chat.LastName
            };

            await WriteUserData(userChatData);

            await _next(botUpdateContext);
        }

        private async Task WriteUserData(ChatUserData chatUserData)
        {
            var data = await ReadUserData();
            if (data.Any(x => chatUserData.Id == x.Id &&
                              chatUserData.Username == x.Username &&
                              chatUserData.FirstName == x.FirstName &&
                              chatUserData.LastName == x.LastName))
                return;

            data.Add(chatUserData);

            var dataStr = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync("./users.json", dataStr);
        }

        private async Task<List<ChatUserData>> ReadUserData()
        {
            try
            {
                var dataStr = await File.ReadAllTextAsync("./users.json");
                return JsonConvert.DeserializeObject<List<ChatUserData>>(dataStr);
            }
            catch (Exception e)
            {
                return new List<ChatUserData>();
            }
        }
    }
}