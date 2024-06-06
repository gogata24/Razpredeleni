using BSCars2.Entities;
using BSCars2.ProjectDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BSCars2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly BSCars2DbContext _context;
        public CarsController(BSCars2DbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Cars>>> AddCar(Cars car)
        {
            var dbCar = new Cars()
            {
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                Description = car.Description,
                UserId = car.UserId

            };
            _context.Cars.Add(dbCar);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<Cars>>> GetAllCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return Ok(cars);
        }

        [HttpGet]
        public async Task<ActionResult<List<Cars>>> GetCarDetails(int id)
        {
            var car = await _context.Cars.SingleOrDefaultAsync(x => x.Id == id);
            return Ok(car);
        }



        [HttpPut]
        public async Task<ActionResult<List<Cars>>> UpdateCars(Cars updatedCar)
        {
            var dbCar = await _context.Cars.FindAsync(updatedCar.Id);
            if (dbCar is null)
            {
                return NotFound("Car not found");
            }
            dbCar.Brand = updatedCar.Brand;
            dbCar.Model = updatedCar.Model;
            dbCar.Year = updatedCar.Year;
            dbCar.Price = updatedCar.Price;
            dbCar.Description = updatedCar.Description;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult<List<Cars>>> DeleteCar(int id)
        {
            var dbCar = await _context.Cars.FindAsync(id);
            if (dbCar is null)
            {
                return NotFound("Car not found");
            }
            _context.Cars.Remove(dbCar);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
