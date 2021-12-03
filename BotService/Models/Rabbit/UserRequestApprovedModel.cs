using System;

namespace BotService.Models.Rabbit
{
    public class UserRequestApprovedModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Username { get; set; }
        public string ApprovedBy { get; set; }
    }
}
