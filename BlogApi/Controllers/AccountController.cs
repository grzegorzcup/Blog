using Application.DTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        [HttpGet]
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

        }
    }
}
