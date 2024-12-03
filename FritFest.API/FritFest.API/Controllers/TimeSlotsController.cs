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
    public class TimeSlotsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public TimeSlotsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TijdStips
        [HttpGet]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<TimeSlotDto>>> GetTimeSlots()
        {
            var timeSlots = await _context.TimeSlots
                .Include(ts => ts.Artist)
                .Include(ts => ts.Stage)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots));
        }

        // GET: api/TijdStips/5
        [HttpGet("{id}")]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<TimeSlotDto>> GetTimeSlot(Guid id)
        {
            var timeSlot = await _context.TimeSlots
                .Include(ts => ts.Artist)
                .Include(ts => ts.Stage)
                .FirstOrDefaultAsync(ts => ts.TimeSlotId == id);

            if (timeSlot == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TimeSlotDto>(timeSlot));
        }

        // PUT: api/TijdStips/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutTimeSlot(Guid id, TimeSlotDto dto)
        {
            if (id != dto.ArtistId)
            {
                return BadRequest();
            }

            var tijdStip = _mapper.Map<TimeSlot>(dto);
            _context.Entry(tijdStip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeSlotExists(id))
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

        // POST: api/TijdStips
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<TimeSlotDto>> PostTimeSlot(TimeSlotDto dto)
        {
            var timeSlot = _mapper.Map<TimeSlot>(dto);
            timeSlot.TimeSlotId = Guid.NewGuid();
            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTimeSlot), new { id = timeSlot.ArtistId }, _mapper.Map<TimeSlotDto>(timeSlot));
        }

        // DELETE: api/TijdStips/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteTimeSlot(Guid id)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(id);
            if (timeSlot == null)
            {
                return NotFound();
            }

            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TimeSlotExists(Guid id)
        {
            return _context.TimeSlots.Any(e => e.ArtistId == id);
        }
    }
}
