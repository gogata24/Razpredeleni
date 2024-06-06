using BSCars2.Entities;
using BSCars2.ProjectDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BSCars2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BSCars2DbContext _context;
        public UsersController(BSCars2DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Users>>> AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        [HttpPut]
        public async Task<ActionResult<List<Users>>> UpdateUser(Users updatedUser)
        {
            var dbUser = await _context.Users.FindAsync(updatedUser.Id);
            if (dbUser is null)
            {
                return NotFound("User not found");
            }
            dbUser.Username = updatedUser.Username;
            dbUser.Password = updatedUser.Password;
            dbUser.Email = updatedUser.Email;
            dbUser.Phone = updatedUser.Phone;
            dbUser.City = updatedUser.City;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("id")]
        public async Task<ActionResult<List<Users>>> DeleteUser(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser is null)
            {
                return NotFound("User not found");
            }
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
