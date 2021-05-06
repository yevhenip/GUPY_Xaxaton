using Gupy.Api.Entities;

namespace Gupy.Api.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
        public int SubscribedCount { get; set; }
        public int MinWantedPeople { get; set; }
        public string EventTime { get; set; }
        public double Duration { get; set; }
    }
}