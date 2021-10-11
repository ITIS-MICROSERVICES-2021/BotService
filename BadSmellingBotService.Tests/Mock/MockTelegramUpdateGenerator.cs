using BadSmellingBotService.Tests.Constants;
using Telegram.Bot.Types;

namespace BadSmellingBotService.Tests.Mock
{
    public class MockTelegramUpdateGenerator
    {
        public static Update Generate()
            => new Update
            {
                Message = new Message
                {
                    Chat = new Chat()
                }
            };
    }
}