using System.Collections.Generic;
using System.Threading.Tasks;
using Gupy.Api.Entities;
using Gupy.Api.Models;

namespace Gupy.Api.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<EventModel>> GetAllAsync();
        Task<EventModel> GetAsync(int id);
        Task CreateAsync(Event @event);
        Task DeleteAsync(int id);
        Task<IEnumerable<EventModel>> GetPageAsync(int page);
        public Task<bool> UpdateSubscribersCountAsync(int eventId, int userId);
    }
}