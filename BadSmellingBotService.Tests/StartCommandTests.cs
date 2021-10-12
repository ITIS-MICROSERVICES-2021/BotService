using System.Threading;
using BadSmellingBotService.Tests.Constants;
using BadSmellingBotService.Tests.Mock;
using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.CommandHandlers;
using Xunit;

namespace BadSmellingBotService.Tests
{
    public class StartCommandTests
    {
        [Fact]
        public void TestExpectedMessage()
        {
            var mockClient = MockTelegramBotClientGenerator.Generate();
            var mockUpdate = MockTelegramUpdateGenerator.Generate();
            
            mockUpdate.Message.Chat.Id = MockConstants.ChatId;
            var expectedMessage = "Приветствуем вас в боте";

            var testSubject = new StartCommandHandler();
            testSubject.HandleAsync(mockClient, mockUpdate, new CancellationToken());
            
            Assert.Contains(expectedMessage, mockClient.MockSentMessagesStorage[MockConstants.ChatId]);
        }
    }
}