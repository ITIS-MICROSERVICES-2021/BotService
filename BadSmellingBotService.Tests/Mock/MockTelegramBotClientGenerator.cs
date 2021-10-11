using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

namespace BadSmellingBotService.Tests.Mock
{
    public static class MockTelegramBotClientGenerator
    {
        public static MockTelegramBotClient Generate()
            => new MockTelegramBotClient();
    }

    public class MockTelegramBotClient : ITelegramBotClient
    {
        public Dictionary<long, List<string>> MockSentMessagesStorage = new();

        public Task<TResponse> MakeRequestAsync<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var sendTextMessageRequest = request as SendMessageRequest;
            var response = default(TResponse);

            if (sendTextMessageRequest != null)
            {
                SaveTextMessageToStorage(sendTextMessageRequest);
                
                return Task.FromResult(response);
            }

            throw new NotImplementedException();
        }

        public void SaveTextMessageToStorage(SendMessageRequest request)
        {
            var chatId = request.ChatId.Identifier ?? 0;
            
            if(!MockSentMessagesStorage.ContainsKey(chatId))
                MockSentMessagesStorage.Add(chatId, new List<string>());
            
            MockSentMessagesStorage[chatId].Add(request.Text);
        }

        public Task<bool> TestApiAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task DownloadFileAsync(string filePath, Stream destination,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public long? BotId { get; }
        public TimeSpan Timeout { get; set; }
        public IExceptionParser ExceptionsParser { get; set; }
        public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;
        public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;
    }
}