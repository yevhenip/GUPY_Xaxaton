using System.Collections.Generic;

namespace Gupy.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string TelegramId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public IEnumerable<UserEvent> Events { get; set; } = new List<UserEvent>();
        public string Phone { get; set; }
    }
}