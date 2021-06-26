using BackEnd.Entities;
using BackEnd.Models;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;

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

        //[HttpGet(Name = "GetUsers")]
        //public ActionResult<IEnumerable<User>> Get() => Ok(_userService.Get());

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> Get(Guid id)

        {
            //var user = _userService.Get(id);
            //if (user is not null)
            //{
            //    return user;
            //}
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            //_userService.Remove(id);
            return Ok();
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
        public IActionResult Update(Guid id, UserRequest request)
        {
            // _userService.Update(id, new User { Name = request.Name, Surname = request.Surname, Password = request.Password });
            return Ok();
        }
    }
}