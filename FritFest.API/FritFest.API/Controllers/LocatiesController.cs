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
    public class LocatiesController : ControllerBase
    {
        private readonly FestivalContext _context;

        public LocatiesController(FestivalContext context)
        {
            _context = context;
        }

        // GET: api/Locaties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locatie>>> GetLocatie()
        {
            return await _context.Locatie.ToListAsync();
        }

        // GET: api/Locaties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locatie>> GetLocatie(Guid id)
        {
            var locatie = await _context.Locatie.FindAsync(id);

            if (locatie == null)
            {
                return NotFound();
            }

            return locatie;
        }

        // PUT: api/Locaties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocatie(Guid id, Locatie locatie)
        {
            if (id != locatie.LocatieId)
            {
                return BadRequest();
            }

            _context.Entry(locatie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocatieExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Locatie>> PostLocatie(Locatie locatie)
        {
            _context.Locatie.Add(locatie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocatie", new { id = locatie.LocatieId }, locatie);
        }

        // DELETE: api/Locaties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocatie(Guid id)
        {
            var locatie = await _context.Locatie.FindAsync(id);
            if (locatie == null)
            {
                return NotFound();
            }

            _context.Locatie.Remove(locatie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocatieExists(Guid id)
        {
            return _context.Locatie.Any(e => e.LocatieId == id);
        }
    }
}
