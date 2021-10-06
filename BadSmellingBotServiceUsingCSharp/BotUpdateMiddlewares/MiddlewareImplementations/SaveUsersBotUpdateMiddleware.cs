using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BadSmellingBotServiceUsingCSharp.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.MiddlewareImplementations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SaveUsersBotUpdateMiddleware : BotUpdateMiddleware
    {
        public SaveUsersBotUpdateMiddleware(BotUpdateDelegate next) : base(next)
        {
        }

        public override async Task InvokeAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            var userChatData = new ChatUserData()
            {
                Id = update.Message?.Chat.Id ?? -1,
                Username = update.Message?.Chat.Username,
                FirstName = update.Message?.Chat.FirstName,
                LastName = update.Message?.Chat.LastName
            };

            await WriteUserData(userChatData);

            await Next(botClient, update, cancellationToken);
        }

        private async Task WriteUserData(ChatUserData chatUserData)
        {
            var data = await ReadUserData();
            if (data.Any(x => chatUserData.Id == x.Id &&
                              chatUserData.Username == x.Username &&
                              chatUserData.FirstName == x.FirstName &&
                              chatUserData.LastName == x.LastName))
            {
                return;
            }

            data.Add(chatUserData);

            var dataStr = JsonConvert.SerializeObject(data);
            await System.IO.File.WriteAllTextAsync("./users.json", dataStr);
        }

        private async Task<List<ChatUserData>> ReadUserData()
        {
            try
            {
                var dataStr = await System.IO.File.ReadAllTextAsync("./users.json");
                return JsonConvert.DeserializeObject<List<ChatUserData>>(dataStr);
            }
            catch (Exception e)
            {
                return new List<ChatUserData>();
            }
        }
    }
}