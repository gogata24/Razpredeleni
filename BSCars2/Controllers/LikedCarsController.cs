using BSCars2.Entities;
using BSCars2.ProjectDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BSCars2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LikedCarsController : ControllerBase
    {
        private readonly BSCars2DbContext _context;
        public LikedCarsController(BSCars2DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<LikedCars>>> AddLikedCar(LikedCars car)
        {
            var dbCar = new LikedCars()
            {
                CarId = car.CarId,
                UserId = car.UserId,
                DateOfLiked = DateTime.Now,

            };
            _context.LikedCars.Add(dbCar);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<LikedCars>>> GetAllLikedCars()
        {
            var cars = await _context.LikedCars.ToListAsync();
            return Ok(cars);
        }
        [HttpPut]
        public async Task<ActionResult<List<LikedCars>>> UpdateLikeCars(LikedCars updatedCar)
        {
            var dbCar = await _context.LikedCars.FindAsync(updatedCar.Id);
            if (dbCar is null)
            {
                return NotFound("Liked Car not found");
            }
            dbCar.DateOfLiked = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult<List<LikedCars>>> DeleteLikedCar(int id)
        {
            var dbCar = await _context.LikedCars.FindAsync(id);
            if (dbCar is null)
            {
                return NotFound("Liked car not found");
            }
            _context.LikedCars.Remove(dbCar);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
