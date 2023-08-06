using Application.DTO;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserDto userDto) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(userDto == null)
            {
                return NotFound();
            }
            var user =_userService.RegisterUser(userDto);
            return Ok();

        }
        [HttpPost("login")]
        public IActionResult Login(LoginDto login)
        {
            var token = _userService.Login(login);
            if(token == null)
            {
                return NotFound();
            }
            return Ok(token);
        }

    }
}
