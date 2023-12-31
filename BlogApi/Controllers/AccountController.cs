﻿using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin,User")]
    //[Authorize]
    //[Authorize(Policy = "HasEmail")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public IActionResult RegisterUser(RegisterUserDto userDto) 
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            if(userDto == null) 
                return NotFound();
            
            var user =_userService.RegisterUser(userDto);
            return Ok();

        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto login)
        {
            var token = _userService.Login(login);
            if(token == null)
            {
                return NotFound();
            }
            return Ok(token);
        }
        [HttpDelete("RemoveUser")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult RemoveUser(int id) 
        {
            var result = _userService.Remove(id);
            if(result)
                return Ok();
            return NotFound();
        }
        [HttpPost("UpdateUser")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult UpdateUser(int id,UpdateUserDto userDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            _userService.Update(id,userDto);
            return Ok();
        }
        [HttpGet("GetUser")]
        [Authorize(Roles = "Admin,User,Moderator")]
        public IActionResult GetUser(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = _userService.GetById(id);
            return Ok(user);
        }
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult GetAllUsers()
        {
            var list = _userService.GetUsers();
            return Ok(list);
        }
    }
}
