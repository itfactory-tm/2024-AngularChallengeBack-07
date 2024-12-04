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
using System.Net.Sockets;
using Microsoft.AspNetCore.Authorization;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoughtTicketsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public BoughtTicketsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/GekochteTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoughtTicketDto>>> GetBoughtTickets()
        {
            var boughtTickets = await _context.BoughtTicket
                .Include(gt => gt.Ticket)
                .ThenInclude(t => t.TicketType)
                .Include(gt => gt.Edition)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<BoughtTicketDto>>(boughtTickets));
        }

        // GET: api/GekochteTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoughtTicketDto>> GetBoughtTicket(Guid id)
        {
            var boughtTicket = await _context.BoughtTicket
                .Include(gt => gt.Ticket)
                .ThenInclude(t => t.TicketType)
                .FirstOrDefaultAsync(t => t.BoughtTicketId == id);

            if (boughtTicket == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TicketDto>(boughtTicket));
        }

        // PUT: api/GekochteTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutBoughtTicket(Guid id, BoughtTicketDto dto)
        {
            if (id != dto.BoughtTicketId)
            {
                return BadRequest();
            }

            var ticket = _mapper.Map<Ticket>(dto);

            _context.Entry(dto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoughtTicketExists(id))
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

        // POST: api/GekochteTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        public async Task<ActionResult<BoughtTicketDto>> PostBoughtTicket(BoughtTicketDto dto)
        {
            var ticket = _mapper.Map<BoughtTicket>(dto);
            ticket.TicketId = Guid.NewGuid();
            _context.BoughtTicket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoughtTicket), new { id = ticket.BoughtTicketId}, _mapper.Map<TicketDto>(ticket));
        }

        // DELETE: api/GekochteTickets/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteBoughtTicket(Guid id)
        {
            var ticket = await _context.BoughtTicket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.BoughtTicket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoughtTicketExists(Guid id)
        {
            return _context.BoughtTicket.Any(e => e.BoughtTicketId == id);
        }
    }
}
