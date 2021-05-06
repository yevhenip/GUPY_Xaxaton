using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Gupy.Api.Entities;
using Gupy.Api.Interfaces;
using Gupy.Api.Interfaces.Repositories;
using Gupy.Api.Models;

namespace Gupy.Api.Concrete.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbConnectionFactory _dbConnection;

        public EventRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnection = dbConnectionFactory;
        }

        public async Task<IEnumerable<EventModel>> GetAllAsync()
        {
            using var connection = _dbConnection.CreateConnection();
            var events = (await connection.QueryAsync<Event>("select * from events")).ToList();
            var result = events.Select(@event => new EventModel
                {
                    Id = @event.Id,
                    Description = @event.Description,
                    Name = @event.Name,
                    Duration = @event.Duration,
                    Type = @event.Type,
                    EventTime = @event.EventTime.ToString("f"),
                    SubscribedCount = @event.SubscribedCount,
                    MinWantedPeople = @event.MinWantedPeople
                })
                .ToList();

            return result;
        }

        public async Task<EventModel> GetAsync(int id)
        {
            using var connection = _dbConnection.CreateConnection();
            var @event =
                await connection.QuerySingleOrDefaultAsync<Event>("select * from events where Id = @Id", new {Id = id});
            var date = @event.EventTime.ToString("F");
            @event.EventTime = Convert.ToDateTime(date);
            return new EventModel
            {
                Id = @event.Id, Description = @event.Description, Name = @event.Name, Duration = @event.Duration,
                Type = @event.Type, EventTime = @event.EventTime.ToString("f"),
                SubscribedCount = @event.SubscribedCount, MinWantedPeople = @event.MinWantedPeople
            };
        }

        public Task CreateAsync(Event @event)
        {
            using var connection = _dbConnection.CreateConnection();
            connection.ExecuteAsync(
                "insert into events(Name, Description, SubscribedCount, MinWantedPeople, Type, EventTime) values(@Name, @Description, @SubscribedCount, @MinWantedPeople, @Category, @Type)",
                new
                {
                    @event.Name, @event.Description, @event.SubscribedCount, @event.MinWantedPeople, @event.EventTime
                });
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            using var connection = _dbConnection.CreateConnection();
            connection.ExecuteAsync(
                "delete from events where Id = @Id", new {Id = id});

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<EventModel>> GetPageAsync(int page)
        {
            using var connection = _dbConnection.CreateConnection();
            var events = (await connection.QueryAsync<Event>("select * from events limit 3 offset @Page",
                new {Page = (page - 1) * 3})).ToList();
            var result = events.Select(@event => new EventModel
                {
                    Id = @event.Id,
                    Description = @event.Description,
                    Name = @event.Name,
                    Duration = @event.Duration,
                    Type = @event.Type,
                    EventTime = @event.EventTime.ToString("f", new CultureInfo("uk-UA")),
                    SubscribedCount = @event.SubscribedCount,
                    MinWantedPeople = @event.MinWantedPeople
                })
                .ToList();

            return result;
        }


        public async Task<bool> UpdateSubscribersCountAsync(int eventId, int userId)
        {
            using var connection = _dbConnection.CreateConnection();
            var id =
                await connection.QuerySingleOrDefaultAsync<int?>(
                    "select u.Id from events e join userEvents ue on e.Id = ue.EventId join users u on ue.UserId = u.Id where e.Id = @EventId and u.Id = @UserId",
                    new {EventId = eventId, UserId = userId});
            if (id is not null) return false;
            var count =
                await connection.QuerySingleOrDefaultAsync<int>("select SubscribedCount from events where Id = @Id",
                    new {Id = eventId});
            await connection.ExecuteAsync(
                "update events set SubscribedCount = @Count where Id = @Id", new {Count = count + 1, Id = eventId});
            await connection.ExecuteAsync(
                "insert into userEvents(EventId, UserId) values(@EventId, @UserId)",
                new {EventId = eventId, UserId = userId});
            return true;
        }
    }
}