using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Gupy.Api.Entities;
using Gupy.Api.Interfaces;
using Gupy.Api.Interfaces.Repositories;

namespace Gupy.Api.Concrete.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnection;

        public UserRepository(IDbConnectionFactory dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = _dbConnection.CreateConnection();
            return connection.QueryAsync<User>("select * from users");
        }

        public Task<User> GetAsync(int id)
        {
            using var connection = _dbConnection.CreateConnection();
            return connection.QuerySingleOrDefaultAsync<User>("select * from users where TelegramId = @Id", new {Id = id});
        }

        public async Task<bool> IsExists(int id)
        {
            var user = await GetAsync(id);
            return user is not null;
        }

        public Task CreateAsync(User user)
        {
            using var connection = _dbConnection.CreateConnection();
            connection.ExecuteAsync(
                "insert into users(Name, UserName, Phone, TelegramId) values(@Name, @UserName, @Phone, @TelegramId)",
                new
                {
                    user.Name, user.UserName, user.Phone, user.TelegramId
                });
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int telegramId)
        {
            using var connection = _dbConnection.CreateConnection();
            connection.ExecuteAsync(
                "delete from users where TelegramId = @TelegramId", new{TelegramId = telegramId});
               
            return Task.CompletedTask;
        }
    }
}