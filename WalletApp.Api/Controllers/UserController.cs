using Microsoft.AspNetCore.Mvc;
using WalletApp.Application.Interfaces;
using WalletApp.Application.Models;

namespace WalletApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userService.GetListOfUserAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            await _userService.CreateUserAsync(model);

            return Ok();
        }
    }
}


