using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FritFest.API.DbContexts;
using FritFest.API.Entities;
using AutoMapper;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtiestsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public ArtiestsController(FestivalContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Artiests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artiest>>> GetArtiest()
        {
            return await _context.Artiest.ToListAsync();
        }

        // GET: api/Artiests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artiest>> GetArtiest(Guid id)
        {
            var artiest = await _context.Artiest.FindAsync(id);

            if (artiest == null)
            {
                return NotFound();
            }

            return artiest;
        }

        // PUT: api/Artiests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtiest(Guid id, Artiest artiest)
        {
            if (id != artiest.ArtistId)
            {
                return BadRequest();
            }

            _context.Entry(artiest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtiestExists(id))
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

        // POST: api/Artiests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artiest>> PostArtiest(Artiest artiest)
        {
            _context.Artiest.Add(artiest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtiest", new { id = artiest.ArtistId }, artiest);
        }

        // DELETE: api/Artiests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtiest(Guid id)
        {
            var artiest = await _context.Artiest.FindAsync(id);
            if (artiest == null)
            {
                return NotFound();
            }

            _context.Artiest.Remove(artiest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtiestExists(Guid id)
        {
            return _context.Artiest.Any(e => e.ArtistId == id);
        }
    }
}
