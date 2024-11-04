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
    public class PodiumsController : ControllerBase
    {
        private readonly FestivalContext _context;

        public PodiumsController(FestivalContext context)
        {
            _context = context;
        }

        // GET: api/Podiums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Podium>>> GetPodium()
        {
            return await _context.Podium.ToListAsync();
        }

        // GET: api/Podiums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Podium>> GetPodium(Guid id)
        {
            var podium = await _context.Podium.FindAsync(id);

            if (podium == null)
            {
                return NotFound();
            }

            return podium;
        }

        // PUT: api/Podiums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPodium(Guid id, Podium podium)
        {
            if (id != podium.PodiumId)
            {
                return BadRequest();
            }

            _context.Entry(podium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodiumExists(id))
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

        // POST: api/Podiums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Podium>> PostPodium(Podium podium)
        {
            _context.Podium.Add(podium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPodium", new { id = podium.PodiumId }, podium);
        }

        // DELETE: api/Podiums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePodium(Guid id)
        {
            var podium = await _context.Podium.FindAsync(id);
            if (podium == null)
            {
                return NotFound();
            }

            _context.Podium.Remove(podium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PodiumExists(Guid id)
        {
            return _context.Podium.Any(e => e.PodiumId == id);
        }
    }
}
