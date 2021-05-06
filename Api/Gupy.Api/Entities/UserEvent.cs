using System.Collections.Generic;

namespace Gupy.Api.Entities
{
    public class UserEvent
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public List<Event> Events { get; set; }
        public int UserId { get; set; }
        public List<User> Users { get; set; }
    }
}