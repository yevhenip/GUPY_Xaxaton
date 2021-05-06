using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Gupy.Api.Entities;
using Gupy.Api.Interfaces.Repositories;

namespace Gupy.Api.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbConnection _dbConnection;

        public EventRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<Event>> GetAllAsync()
        {
            return _dbConnection.QueryAsync<Event>("select * from events");
        }

        public Task<Event> GetAsync(int id)
        {
            return _dbConnection.QuerySingleOrDefaultAsync<Event>("select * from events where id = @id", new {Id = id});
        }

        public Task CreateAsync(Event @event)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}