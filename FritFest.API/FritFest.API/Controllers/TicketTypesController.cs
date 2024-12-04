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
    public class TicketTypesController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public TicketTypesController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TicketTypes
        [HttpGet]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<TicketTypeDto>>> GetTicketTypes()
        {
            var ticketTypes = await _context.TicketTypes
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TicketTypeDto>>(ticketTypes));
        }

        // GET: api/TicketTypes/5
        [HttpGet("{id}")]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<TicketTypeDto>> GetTicketType(Guid id)
        {
            var ticketType = await _context.TicketTypes
                .FirstOrDefaultAsync(tt => tt.TicketTypeId == id);

            if (ticketType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TicketTypeDto>(ticketType));
        }

        // PUT: api/TicketTypes/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutTicketType(Guid id, TicketTypeDto ticketTypeDto)
        {
            if (id != ticketTypeDto.TicketTypeId)
            {
                return BadRequest();
            }

            var ticketType = _mapper.Map<TicketType>(ticketTypeDto);
            _context.Entry(ticketType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketTypeExists(id))
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

        // POST: api/TicketTypes
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<TicketTypeDto>> PostTicketType(TicketTypeDto ticketTypeDto)
        {
            var ticketType = _mapper.Map<TicketType>(ticketTypeDto);
            ticketType.TicketTypeId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.TicketTypes.Add(ticketType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketType), new { id = ticketType.TicketTypeId }, _mapper.Map<TicketTypeDto>(ticketType));
        }

        // DELETE: api/TicketTypes/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteTicketType(Guid id)
        {
            var ticketType = await _context.TicketTypes.FindAsync(id);
            if (ticketType == null)
            {
                return NotFound();
            }

            _context.TicketTypes.Remove(ticketType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketTypeExists(Guid id)
        {
            return _context.TicketTypes.Any(e => e.TicketTypeId == id);
        }
    }
}
