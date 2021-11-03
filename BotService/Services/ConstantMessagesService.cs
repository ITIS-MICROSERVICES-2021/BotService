// ReSharper disable MemberCanBeMadeStatic.Global

#pragma warning disable CA1822
namespace BotService.Services
{
    public class ConstantMessagesService
    {
        public string AvailableCommands => "Доступные команды";
        public string Greetings => "Приветствуем Вас в боте";

        public string Help => "/start для приветствия\n" +
                              "/help для получения списка команд\n"+
                              "Хочу в отпуск с DD.MM.YYYY по DD.MM.YYYY для отправки заявки на отпуск";
    }
}
#pragma warning restore CA1822