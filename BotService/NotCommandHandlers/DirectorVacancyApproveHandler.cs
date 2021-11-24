using System;
using System.Threading.Tasks;
using BotService.Models;
using CoreDTO.Redis;
using RedisIO.Services;
using Telegram.Bot.Host.CommandHandlerMiddleware;
using Telegram.Bot.Host.CommandHandlerMiddleware.CommandHandlers;
using Telegram.Bot.Host.Middleware;

namespace BotService.NotCommandHandlers
{
    [BotCommand(CommandText = "DirectorVacancyApprove")]
    public class DirectorVacancyApproveHandler : ICommandHandler
    {
        private readonly IRedisIOService _redisIoService;

        public DirectorVacancyApproveHandler(IRedisIOService redisIoService)
        {
            _redisIoService = redisIoService;
        }

        public async Task HandleAsync(BotUpdateContext botUpdateContext)
        {
            var message = botUpdateContext.Update.Message.Text;
            var chatId = botUpdateContext.Update.Message!.Chat.Id;
            var splitMessage = message.Split(" ");
            try
            {
                var request = await _redisIoService.GetAsync<RequestDto<EStatus, EPayload>>(splitMessage[2]);
                request.Status = EStatus.Approved;
                await _redisIoService.AddAsync(request.Id.ToString(), request);
                await botUpdateContext.BotClient.SendTextMessageAsync(chatId, "Заявка подтверждена",
                    cancellationToken: botUpdateContext.CancellationToken);
            }
            catch (Exception _)
            {
                await botUpdateContext.BotClient.SendTextMessageAsync(chatId, "Не удалось подтвердить заявку",
                    cancellationToken: botUpdateContext.CancellationToken);
            }
        }
    }
}