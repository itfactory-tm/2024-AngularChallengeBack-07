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

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GekochteTicketsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public GekochteTicketsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/GekochteTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GekochteTicketDto>>> GetGekochteTicket()
        {
            var gekochteTickets = await _context.GekochteTicket
                .Include(gt => gt.Ticket)
                .ThenInclude(t => t.TicketType)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<GekochteTicketDto>>(gekochteTickets));
        }

        // GET: api/GekochteTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GekochteTicket>> GetGekochteTicket(Guid id)
        {
            var gekochteTicket = await _context.GekochteTicket.FindAsync(id);

            if (gekochteTicket == null)
            {
                return NotFound();
            }

            return gekochteTicket;
        }

        // PUT: api/GekochteTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGekochteTicket(Guid id, GekochteTicket gekochteTicket)
        {
            if (id != gekochteTicket.GekochteTicketId)
            {
                return BadRequest();
            }

            _context.Entry(gekochteTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GekochteTicketExists(id))
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
        public async Task<ActionResult<GekochteTicket>> PostGekochteTicket(GekochteTicket gekochteTicket)
        {
            _context.GekochteTicket.Add(gekochteTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGekochteTicket", new { id = gekochteTicket.GekochteTicketId }, gekochteTicket);
        }

        // DELETE: api/GekochteTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGekochteTicket(Guid id)
        {
            var gekochteTicket = await _context.GekochteTicket.FindAsync(id);
            if (gekochteTicket == null)
            {
                return NotFound();
            }

            _context.GekochteTicket.Remove(gekochteTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GekochteTicketExists(Guid id)
        {
            return _context.GekochteTicket.Any(e => e.GekochteTicketId == id);
        }
    }
}
