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
    public class LocationsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public LocationsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Locaties
        [HttpGet]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
        {
            var locations = await _context.Locations
                .Include(l => l.FoodTrucks)
                .Include(l => l.Stages)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<LocationDto>>(locations));
        }

        // GET: api/Locaties/5
        [HttpGet("{id}")]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<LocationDto>> GetLocation(Guid id)
        {
            var location = await _context.Locations
                .Include(l => l.FoodTrucks)
                .Include(l => l.Stages)
                .FirstOrDefaultAsync(l => l.LocationId== id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LocationDto>(location));
        }

        // PUT: api/Locaties/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutLocation(Guid id, LocationDto locationDto)
        {
            if (id != locationDto.LocationId)
            {
                return BadRequest();
            }

            var location = _mapper.Map<Location>(locationDto);
            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locaties
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<LocationDto>> PostLocation(LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            location.LocationId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocation), new { id = location.LocationId }, _mapper.Map<LocationDto>(location));
        }

        // DELETE: api/Locaties/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(Guid id)
        {
            return _context.Locations.Any(e => e.LocationId == id);
        }
    }
}
