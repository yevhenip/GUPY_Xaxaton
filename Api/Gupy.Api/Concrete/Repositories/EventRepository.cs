using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Gupy.Api.Entities;
using Gupy.Api.Interfaces;
using Gupy.Api.Interfaces.Repositories;

namespace Gupy.Api.Concrete.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbConnectionFactory _dbConnection;

        public EventRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnection = dbConnectionFactory;
        }

        public Task<IEnumerable<Event>> GetAllAsync()
        {
            using var connection = _dbConnection.CreateConnection();
            return connection.QueryAsync<Event>("select * from events");
        }

        public Task<Event> GetAsync(int id)
        {
            using var connection = _dbConnection.CreateConnection();
            return connection.QuerySingleOrDefaultAsync<Event>("select * from events where Id = @Id", new {Id = id});
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
                "delete from events where Id = id");
               
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Event>> GetPageAsync(int page)
        {
            using var connection = _dbConnection.CreateConnection();
            return connection.QueryAsync<Event>("select * from events limit 3 offset @Page", new {Page = (page - 1) * 3});
        }
    }
}