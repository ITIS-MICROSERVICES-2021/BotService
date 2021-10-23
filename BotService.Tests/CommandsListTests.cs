using System.Reflection;
using BotService.Services;
using Xunit;

namespace BotService.Tests
{
    public class ConstantMessagesServiceTests
    {
        [Fact]
        public void TestExpectedMessage()
        {
            var service = new ConstantMessagesService();

            var count = 0;
            var properties = typeof(ConstantMessagesService).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(string))
                    continue;
                count++;
                var value = (string)property.GetValue(service);
                Assert.True(value is { Length: > 0 });
            }

            Assert.True(count > 0);
        }
    }
}