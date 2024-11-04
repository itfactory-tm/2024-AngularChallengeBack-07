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
    public class ArtikelsController : ControllerBase
    {
        private readonly FestivalContext _context;

        public ArtikelsController(FestivalContext context)
        {
            _context = context;
        }

        // GET: api/Artikels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artikel>>> GetArtikel()
        {
            return await _context.Artikel.ToListAsync();
        }

        // GET: api/Artikels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artikel>> GetArtikel(Guid id)
        {
            var artikel = await _context.Artikel.FindAsync(id);

            if (artikel == null)
            {
                return NotFound();
            }

            return artikel;
        }

        // PUT: api/Artikels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikel(Guid id, Artikel artikel)
        {
            if (id != artikel.ArtikelId)
            {
                return BadRequest();
            }

            _context.Entry(artikel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikelExists(id))
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

        // POST: api/Artikels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artikel>> PostArtikel(Artikel artikel)
        {
            _context.Artikel.Add(artikel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtikel", new { id = artikel.ArtikelId }, artikel);
        }

        // DELETE: api/Artikels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtikel(Guid id)
        {
            var artikel = await _context.Artikel.FindAsync(id);
            if (artikel == null)
            {
                return NotFound();
            }

            _context.Artikel.Remove(artikel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtikelExists(Guid id)
        {
            return _context.Artikel.Any(e => e.ArtikelId == id);
        }
    }
}
