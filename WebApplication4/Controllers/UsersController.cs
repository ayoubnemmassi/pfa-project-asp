using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StudentContext _context;

        public UsersController(StudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _context.Users.ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]Users user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                user.Id = Guid.NewGuid();   
                _context.Add(user);
                _context.SaveChanges();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
