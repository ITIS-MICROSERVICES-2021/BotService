// ReSharper disable MemberCanBeMadeStatic.Global

#pragma warning disable CA1822
using System.Collections.Generic;

namespace BotService.Services
{
    public class CommandsListService
    {
        public IEnumerable<string> GetCommands()
        {
            return new[]
            {
                "/start",
                "/help"
            };
        }
    }
}
#pragma warning restore CA1822