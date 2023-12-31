﻿using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,User")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost("Add")]
        [Authorize(Roles = "Admin,User,Moderator")]
        public IActionResult AddPost(PostDto postDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(postDto == null)
            {
                return NotFound();
            }
            var post = _postService.AddPost(postDto);
            return Ok(post);
        }
        [HttpDelete("Remove")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Delete(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id == null || id == 0)
            {
                return NotFound();
            }
            _postService.RemovePost(id);
            return Ok();
        }
        [HttpPost("Update")]
        [Authorize(Roles = "Admin,User,Moderator")]
        public IActionResult Update(int  id,string description)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _postService.UpdatePost(id,description);
            return Ok(post);
        }
        [HttpGet("GetPost")]
        [Authorize(Roles = "Admin,User,Moderator")]
        public IActionResult GetPost(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var post = _postService.GetPost(id);
            return Ok(post);
        }
        [HttpGet("GetAllPosts")]
        [Authorize(Roles = "Admin,User,Moderator")]
        public IActionResult GetAllPosts()
        {
            var list = _postService.GetAllPosts();
            return Ok(list);
        }
    }
}
