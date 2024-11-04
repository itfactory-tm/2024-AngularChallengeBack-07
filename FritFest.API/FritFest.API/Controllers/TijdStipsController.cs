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
    public class TijdStipsController : ControllerBase
    {
        private readonly FestivalContext _context;

        public TijdStipsController(FestivalContext context)
        {
            _context = context;
        }

        // GET: api/TijdStips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TijdStip>>> GetTijdStip()
        {
            return await _context.TijdStip.ToListAsync();
        }

        // GET: api/TijdStips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TijdStip>> GetTijdStip(Guid id)
        {
            var tijdStip = await _context.TijdStip.FindAsync(id);

            if (tijdStip == null)
            {
                return NotFound();
            }

            return tijdStip;
        }

        // PUT: api/TijdStips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTijdStip(Guid id, TijdStip tijdStip)
        {
            if (id != tijdStip.ArtiestId)
            {
                return BadRequest();
            }

            _context.Entry(tijdStip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TijdStipExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TijdStip>> PostTijdStip(TijdStip tijdStip)
        {
            _context.TijdStip.Add(tijdStip);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TijdStipExists(tijdStip.ArtiestId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTijdStip", new { id = tijdStip.ArtiestId }, tijdStip);
        }

        // DELETE: api/TijdStips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTijdStip(Guid id)
        {
            var tijdStip = await _context.TijdStip.FindAsync(id);
            if (tijdStip == null)
            {
                return NotFound();
            }

            _context.TijdStip.Remove(tijdStip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TijdStipExists(Guid id)
        {
            return _context.TijdStip.Any(e => e.ArtiestId == id);
        }
    }
}
