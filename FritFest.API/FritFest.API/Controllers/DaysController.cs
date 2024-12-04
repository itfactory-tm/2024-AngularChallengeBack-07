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
    public class DaysController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public DaysController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Dags
        [HttpGet]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<DayDto>>> GetDays()
        {
            var days = await _context.Days.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DayDto>>(days));
        }

        // GET: api/Dags/5
        [HttpGet("{id}")]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<DayDto>> GetDay(Guid id)
        {
            var day = await _context.Days.FindAsync(id);

            if (day == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DayDto>(day));
        }

        // PUT: api/Dags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutDay(Guid id, DayDto dayDto)
        {
            if (id != dayDto.DayId)
            {
                return BadRequest();
            }

            var day = _mapper.Map<Day>(dayDto);
            _context.Entry(day).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
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

        // POST: api/Dags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<DayDto>> PostDay(DayDto dayDto)
        {
            var day = _mapper.Map<Day>(dayDto);
            day.DayId = Guid.NewGuid();
            _context.Days.Add(day);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDay), new { id = day.DayId }, _mapper.Map<DayDto>(day));
        }

        // DELETE: api/Dags/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteDay(Guid id)
        {
            var day = await _context.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            _context.Days.Remove(day);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DayExists(Guid id)
        {
            return _context.Days.Any(e => e.DayId == id);
        }
    }
}
