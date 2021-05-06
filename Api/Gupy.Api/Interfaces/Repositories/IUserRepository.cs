using System.Collections.Generic;
using System.Threading.Tasks;
using Gupy.Api.Entities;

namespace Gupy.Api.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(int id);
        Task<bool> IsExists(int id);
        Task CreateAsync(User user);
        Task DeleteAsync(int id);
    }
}