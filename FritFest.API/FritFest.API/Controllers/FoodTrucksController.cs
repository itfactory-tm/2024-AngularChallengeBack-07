using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FritFest.API.DbContexts;
using FritFest.API.Entities;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTrucksController : ControllerBase
    {
        private readonly FestivalContext _context;

        public FoodTrucksController(FestivalContext context)
        {
            _context = context;
        }

        // GET: api/FoodTrucks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodTruck>>> GetFoodTruck()
        {
            return await _context.FoodTruck.ToListAsync();
        }

        // GET: api/FoodTrucks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodTruck>> GetFoodTruck(Guid id)
        {
            var foodTruck = await _context.FoodTruck.FindAsync(id);

            if (foodTruck == null)
            {
                return NotFound();
            }

            return foodTruck;
        }

        // PUT: api/FoodTrucks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodTruck(Guid id, FoodTruck foodTruck)
        {
            if (id != foodTruck.FoodTruckId)
            {
                return BadRequest();
            }

            _context.Entry(foodTruck).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodTruckExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FoodTrucks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodTruck>> PostFoodTruck(FoodTruck foodTruck)
        {
            _context.FoodTruck.Add(foodTruck);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodTruck", new { id = foodTruck.FoodTruckId }, foodTruck);
        }

        // DELETE: api/FoodTrucks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodTruck(Guid id)
        {
            var foodTruck = await _context.FoodTruck.FindAsync(id);
            if (foodTruck == null)
            {
                return NotFound();
            }

            _context.FoodTruck.Remove(foodTruck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodTruckExists(Guid id)
        {
            return _context.FoodTruck.Any(e => e.FoodTruckId == id);
        }
    }
}
