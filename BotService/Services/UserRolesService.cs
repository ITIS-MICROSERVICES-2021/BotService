using BotService.Models;
using Microsoft.Extensions.Configuration;

namespace BotService.Services
{
    public class UserRolesService
    {
        private readonly IConfiguration _configuration;

        public UserRolesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Role GetUserRole(string username)
        {
            return _configuration.GetSection("UserToRole").GetValue<Role>(username);
        }
    }
}