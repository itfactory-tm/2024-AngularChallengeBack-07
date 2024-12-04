using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FritFest.API.DbContexts;
using FritFest.API.Entities;
using FritFest.API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTrucksController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public FoodTrucksController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/FoodTrucks
        [HttpGet]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<FoodTruckDto>>> GetFoodTrucks()
        {
            var foodTrucks = await _context.FoodTrucks
                .Include(ft => ft.Edition)
                .Include(ft => ft.MenuItems)
                .Include(ft => ft.Location)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<FoodTruckDto>>(foodTrucks));
        }

        // GET: api/FoodTrucks/5
        [HttpGet("{id}")]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<FoodTruckDto>> GetFoodTruck(Guid id)
        {
            var foodTruck = await _context.FoodTrucks
                .Include(ft => ft.Edition)
                //.Include(ft => ft.MenuItems)
                .Include(ft => ft.Location)
                .FirstOrDefaultAsync(ft => ft.FoodTruckId == id);

            if (foodTruck == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FoodTruckDto>(foodTruck));
        }

        // PUT: api/FoodTrucks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutFoodTruck(Guid id, FoodTruckDto foodTruckDto)
        {
            if (id != foodTruckDto.FoodTruckId)
            {
                return BadRequest();
            }

            var foodTruck = _mapper.Map<FoodTruck>(foodTruckDto);
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
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<FoodTruckDto>> PostFoodTruck(FoodTruckDto foodTruckDto)
        {
            var foodTruck = _mapper.Map<FoodTruck>(foodTruckDto);
            foodTruck.FoodTruckId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.FoodTrucks.Add(foodTruck);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodTruck), new { id = foodTruck.FoodTruckId }, _mapper.Map<FoodTruckDto>(foodTruck));
        }

        // DELETE: api/FoodTrucks/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteFoodTruck(Guid id)
        {
            var foodTruck = await _context.FoodTrucks.FindAsync(id);
            if (foodTruck == null)
            {
                return NotFound();
            }

            _context.FoodTrucks.Remove(foodTruck);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodTruckExists(Guid id)
        {
            return _context.FoodTrucks.Any(e => e.FoodTruckId == id);
        }
    }
}
