using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Host.BotHost;
using Telegram.Bot.Host.HostBuilderExtensions;
using Host = Telegram.Bot.Host.Hosting.Host;

namespace BotService
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                .Build()
                .RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureBotHostDefaults(botBuilder => { botBuilder.UseStartup<Startup>(); });
            /*.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                    config.AddEnvironmentVariables();

                    if (args != null) config.AddCommandLine(args);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                })*/
        }
        //.UseStartup<Startup>();
    }
}