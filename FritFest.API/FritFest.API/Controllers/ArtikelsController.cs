using AutoMapper;
using FritFest.API.DbContexts;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ArtikelsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public ArtikelsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Artikels
        [HttpGet]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<ArtikelDto>>> GetArtikels()
        {
            var artikels = await _context.Artikel
                .Include(a => a.Editie)  // Include Editie to map Editie.Titel
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ArtikelDto>>(artikels));
        }

        // GET: api/Artikels/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<ArtikelDto>> GetArtikel(Guid id)
        {
            var artikel = await _context.Artikel
                .Include(a => a.Editie)  // Include Editie to map Editie.Titel
                .FirstOrDefaultAsync(a => a.ArtikelId == id);

            if (artikel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArtikelDto>(artikel));
        }

        // POST: api/Artikels
        [HttpPost]
        public async Task<ActionResult<ArtikelDto>> PostArtikel(ArtikelDto artikelDto)
        {
            var artikel = _mapper.Map<Artikel>(artikelDto);
            artikel.ArtikelId = Guid.NewGuid();
            _context.Artikel.Add(artikel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtikel), new { id = artikel.ArtikelId }, _mapper.Map<ArtikelDto>(artikel));
        }

        // PUT: api/Artikels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikel(Guid id, ArtikelDto artikelDto)
        {
            if (id != artikelDto.ArtikelId)
            {
                return BadRequest();
            }

            var artikel = _mapper.Map<Artikel>(artikelDto);
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
