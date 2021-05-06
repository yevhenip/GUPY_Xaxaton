using System.Collections.Generic;
using System.Threading.Tasks;
using Gupy.Api.Entities;

namespace Gupy.Api.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetAsync(int id);
        Task CreateAsync(Event @event);
        Task DeleteAsync(int id);
    }
}