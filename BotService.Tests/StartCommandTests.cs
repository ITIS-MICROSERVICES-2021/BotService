using System.Linq;
using BotService.Services;
using Xunit;

namespace BotService.Tests
{
    public class CommandsListServiceTests
    {
        [Fact]
        public void TestExpectedMessage()
        {
            var service = new CommandsListService();

            var commands = service.GetCommands().ToList();
            Assert.Contains("/start", commands);
            Assert.Contains("/help", commands);
        }
    }
}