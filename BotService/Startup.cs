﻿using BotService.Middlewares;
using BotService.NotCommandHandlers;
using BotService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Extensions;
using RedisIO.Converter;
using RedisIO.ServicesExtensions;
using StackExchange.Redis;
using Telegram.Bot.Host.ApplicationBuilder;
using Telegram.Bot.Host.BotServer;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;

namespace BotService
{
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
                        //Тут надо какой - то хост, взял пока из доков редиса
                        EndPoints = { "localhost:6379" }
                    }));
            services.AddScoped<CommandsListService>();
            services.AddScoped<ConstantMessagesService>();
            services.AddScoped<UserRolesService>();
            services.AddScoped<BotUpdateContext>();
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