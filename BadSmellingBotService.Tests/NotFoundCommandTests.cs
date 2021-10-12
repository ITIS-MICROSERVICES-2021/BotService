using System.Threading;
using BadSmellingBotService.Tests.Constants;
using BadSmellingBotService.Tests.Mock;
using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.HelperHandlers;
using Xunit;

namespace BadSmellingBotService.Tests
{
    public class NotFoundCommandTest
    {
        [Fact]
        public void TestExpectedMessage()
        {
            var mockClient = MockTelegramBotClientGenerator.Generate();
            var mockUpdate = MockTelegramUpdateGenerator.Generate();
            
            mockUpdate.Message.Chat.Id = MockConstants.ChatId;
            var expectedMessage = "Команда не найдена";

            CommandNotFoundHandler.CommandNotFound(mockClient, mockUpdate, new CancellationToken());
            
            Assert.Contains(expectedMessage, mockClient.MockSentMessagesStorage[MockConstants.ChatId]);
        }
    }
}