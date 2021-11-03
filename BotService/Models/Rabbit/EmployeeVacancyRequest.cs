using System;

namespace BotService.Models.Rabbit
{
    public class EmployeeVacancyRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Username { get; set; }
    }
}