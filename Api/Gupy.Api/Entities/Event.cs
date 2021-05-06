namespace Gupy.Api.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EventType Type { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int SubscribedCount { get; set; }
        public int MinWantedPeople { get; set; }
    }
}