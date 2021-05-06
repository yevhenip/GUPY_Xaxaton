using System;
using Gupy.Api.Entities;

namespace Gupy.Api.Models
{
    public class CreateEventModel
    {
        public string Name { get; set; }
        public EventType Type { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int SubscribedCount { get; set; }
        public int MinWantedPeople { get; set; }
        public DateTime EventTime { get; set; }
        public double Duration { get; set; }
        public string City { get; set; }
    }
}