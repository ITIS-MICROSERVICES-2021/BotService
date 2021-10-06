using BadSmellingBotServiceUsingCSharp.BotUpdateMiddlewares.MiddlewareImplementations;
using BadSmellingBotServiceUsingCSharp.Extensions;
using BadSmellingBotServiceUsingCSharp.HostedServices;
using BadSmellingBotServiceUsingCSharp.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BadSmellingBotServiceUsingCSharp
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TelegramOptions>(Configuration.GetSection("Telegram"));

            services.AddScoped<UpdateHandler>();
            
            services.AddStorage()
                .AddBotUpdateMiddleware<SaveUsersBotUpdateMiddleware>()
                .AddBotUpdateMiddleware<SorryOlegBotUpdateMiddleware>()
                .AddBotUpdateMiddleware<CommandHandlerUpdateMiddleware>()
                .Confirm(services);

            services.AddHostedService<TelegramBotService>();
        }
    }
}