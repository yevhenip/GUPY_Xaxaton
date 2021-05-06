using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Base;

namespace Gupy.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserRepository _userRepository;

        public EventsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _eventRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _eventRepository.GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetAsync(Event @event)
        {
            await _eventRepository.CreateAsync(@event);
            return Ok();
        }
    }
}

}