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
using System.Text.Json;
using Microsoft.AspNetCore.RateLimiting;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoughtTicketsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public BoughtTicketsController(FestivalContext context, IMapper mapper, HttpClient httpClient)
        {
            _context = context;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        // GET: api/GekochteTickets
        [HttpGet]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<IEnumerable<BoughtTicketDto>>> GetBoughtTickets()
        {
            var boughtTickets = await _context.BoughtTickets
                .Include(gt => gt.Ticket)
                .ThenInclude(t => t.TicketType)
                .Include(bt => bt.Ticket)
                .ThenInclude(t => t.Edition)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<BoughtTicketDto>>(boughtTickets));
        }

        // GET: api/GekochteTickets/5
        [HttpGet("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<BoughtTicketDto>> GetBoughtTicket(Guid id)
        {
            var boughtTicket = await _context.BoughtTickets
                .Include(gt => gt.Ticket)
                .ThenInclude(t => t.TicketType)
                .Include(gt => gt.Ticket).ThenInclude(e => e.Edition)
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
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<BoughtTicketDto>> PostBoughtTicket(BoughtTicketDto dto)
        {
            var ticket = _mapper.Map<BoughtTicket>(dto);
            ticket.BoughtTicketId = Guid.NewGuid();
            _context.BoughtTickets.Add(ticket);
            await _context.SaveChangesAsync();

            var emailContent = new
            {
                NameReceiver = ticket.HolderName,
                EmailReceiver = ticket.HolderMail,
                Subject = "Your ticket Order",
                Body = "",
                BoughtTicketid = ticket.BoughtTicketId.ToString(),
            };

            var response = await _httpClient.PostAsJsonAsync("https://fritfestapi20241203213221.azurewebsites.net/api/Mail", emailContent);

            if (response.IsSuccessStatusCode)
            {
                // Email sent successfully, return the ticket
                return CreatedAtAction(nameof(GetBoughtTicket), new { id = ticket.BoughtTicketId }, _mapper.Map<BoughtTicketDto>(ticket));
            }
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                // Log or return the error response to get more details
                return StatusCode(500, new { message = "Ticket purchased, but email notification failed.", details = errorMessage });
            }
        }

        // DELETE: api/GekochteTickets/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteBoughtTicket(Guid id)
        {
            var ticket = await _context.BoughtTickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.BoughtTickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoughtTicketExists(Guid id)
        {
            return _context.BoughtTickets.Any(e => e.BoughtTicketId == id);
        }
    }
}
