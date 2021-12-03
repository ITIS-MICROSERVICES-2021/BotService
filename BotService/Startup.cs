using BotService.Middlewares;
using BotService.NotCommandHandlers;
using BotService.Rabbit.Producers;
using BotService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Extensions;
using RedisIO.ServicesExtensions;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.Middleware;
using RedisIO.Converter;
using StackExchange.Redis;
using Telegram.Bot.Host.ApplicationBuilder;
using Telegram.Bot.Host.BotServer;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;

namespace BotService
{
    public class MyClass
    {
        public string Message { get; set; }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCommandHandlers();

            services.Configure<TelegramOptions>(Configuration.GetSection("Telegram"));
            services.AddRabbitMQ();
            services.AddRedisIO<JsonRedisConverter>(builder =>
                builder
                    .UseJsonConverter()
                    .UseConfiguration(new ConfigurationOptions()
                    {
                        EndPoints = { "localhost:6379" }
                    }));
            services.AddScoped<CommandsListService>();
            services.AddScoped<ConstantMessagesService>();
            services.AddScoped<UserRolesService>();
            services.AddScoped<EmployeeVacancyRequestProducer>();
            services.AddScoped<ICommandNotFoundHandler, NotCommandHandlersDispatcher>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseMiddleware<SaveUsersMiddleware>();
            app.UseMiddleware<SorryOlegMiddleware>();

            app.UseCommandHandlers();
        }
    }
}