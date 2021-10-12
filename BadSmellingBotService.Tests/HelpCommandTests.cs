using System.Threading;
using BadSmellingBotService.Tests.Constants;
using BadSmellingBotService.Tests.Mock;
using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.CommandHandlers;
using Xunit;

namespace BadSmellingBotService.Tests
{
    public class HelpCommandTests
    {
        [Fact]
        public void TestExpectedMessage()
        {
            var mockClient = MockTelegramBotClientGenerator.Generate();
            var mockUpdate = MockTelegramUpdateGenerator.Generate();
            
            mockUpdate.Message.Chat.Id = MockConstants.ChatId;
            var expectedMessage = "/start для приветствия\n";

            var testSubject = new HelpCommandHelper();
            testSubject.HandleAsync(mockClient, mockUpdate, new CancellationToken());

            var actualResult = mockClient.MockSentMessagesStorage[MockConstants.ChatId];
            
            Assert.Contains(actualResult, item => item.Contains(expectedMessage));
        }
    }
}