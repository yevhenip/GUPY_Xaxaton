using System.Threading.Tasks;
using Gupy.Api.Entities;
using Gupy.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.Base;

namespace Gupy.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> IsExistsAsync(int id)
        {
            var result = await _userRepository.IsExists(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _userRepository.GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(User user)
        {
            await _userRepository.CreateAsync(user);
            return Ok();
        }

        [HttpDelete("{telegramId:int}")]
        public async Task<IActionResult> DeleteAsync(int telegramId)
        {
            if (telegramId < 0)
            {
                return BadRequest("Id for model cannot be negative!");
            }

            if (_userRepository.GetAsync(telegramId) == null)
            {
                return BadRequest("There is model with such an id!");
            }

            await _userRepository.DeleteAsync(telegramId);
            return Ok();
        }
    }
}