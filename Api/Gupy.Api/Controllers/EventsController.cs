using System.Threading.Tasks;
using Gupy.Api.Entities;
using Gupy.Api.Interfaces.Repositories;
using Gupy.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Base;

namespace Gupy.Api.Controllers
{
    public class EventsController : ApiControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _eventRepository.GetAllAsync();
            return Ok(result);
        }
        
        [HttpGet("page/{page:int}")]
        public async Task<IActionResult> GetPageAsync(int page)
        {
            var result = await _eventRepository.GetPageAsync(page);
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