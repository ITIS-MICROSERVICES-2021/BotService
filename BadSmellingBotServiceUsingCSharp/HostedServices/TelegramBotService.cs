using System.Threading;
using System.Threading.Tasks;
using BadSmellingBotServiceUsingCSharp.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace BadSmellingBotServiceUsingCSharp.HostedServices
{
    public class TelegramBotService : IHostedService
    {
        private readonly TelegramOptions _telegramOptions;
        private readonly UpdateHandler _updateHandler;

        public TelegramBotService(IOptions<TelegramOptions> telegramOptions, UpdateHandler updateHandler)
        {
            _updateHandler = updateHandler;
            _telegramOptions = telegramOptions.Value;
        }
        
        private CancellationTokenSource _cancellationTokenSource;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var botClient = new TelegramBotClient(_telegramOptions.Token);
            
            _cancellationTokenSource = new CancellationTokenSource();
            botClient.StartReceiving(
                new DefaultUpdateHandler(_updateHandler.HandleUpdateAsync, ErrorHandler.HandleErrorAsync),
                cancellationToken: cancellationToken);
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}