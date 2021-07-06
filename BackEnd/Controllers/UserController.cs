using BackEnd.DTOs.Response;
using BackEnd.Entities;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<UserResponse>> Get()
        {
            var users = _userService.Get();

            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<UserResponse> Get(string id)

        {
            var user = _userService.Get(id);
            if (user is not null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = _userService.Delete(id);
            return Ok(result);
        }

        [HttpPut]
        public ActionResult<ObjectId> Create(UserRequest userRequest)
        {
            var user = new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                Surname = userRequest.Surname,
                Password = userRequest.Password
            };

            var result = _userService.Create(user);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(string id, UserRequest request)
        {
            var result = _userService.Update(id, request);
            return Ok(result);
        }
    }
}