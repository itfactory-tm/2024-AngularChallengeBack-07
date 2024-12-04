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
    public class TicketsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public TicketsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tickets
        [HttpGet]
        //[EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
        {
            var tickets = await _context.Ticket
                .Include(t => t.Edition)
                .Include(t => t.TicketType)
                .Include(t => t.Day)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TicketDto>>(tickets));
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        //[EnableRateLimiting("PublicLimiter")]

        public async Task<ActionResult<TicketDto>> GetTicket(Guid id)
        {
            var ticket = await _context.Ticket
                .Include(t => t.TicketType)
                .Include(t => t.Edition)
                .Include(t => t.Day)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TicketDto>(ticket));
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutTicket(Guid id, TicketDto ticketDto)
        {
            if (id != ticketDto.TicketId)
            {
                return BadRequest();
            }

            var ticket = _mapper.Map<Ticket>(ticketDto);
            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<TicketDto>> PostTicket(TicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            ticket.TicketId = Guid.NewGuid();
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, _mapper.Map<TicketDto>(ticket));
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(Guid id)
        {
            return _context.Ticket.Any(e => e.TicketId == id);
        }
    }
}
