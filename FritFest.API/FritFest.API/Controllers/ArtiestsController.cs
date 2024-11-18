using AutoMapper;
using FritFest.API.DbContexts;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtiestsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public ArtiestsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Artiests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtiestDto>>> GetArtiests()
        {
            var artiesten = await _context.Artiest
                .Include(a => a.Genre)  // Include Genre to map GenreNaam
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ArtiestDto>>(artiesten));
        }

        // GET: api/Artiests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtiestDto>> GetArtiest(Guid id)
        {
            var artiest = await _context.Artiest
                .Include(a => a.Genre)  // Include Genre to map GenreNaam
                .FirstOrDefaultAsync(a => a.ArtiestId == id);

            if (artiest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArtiestDto>(artiest));
        }

        // POST: api/Artiests
        [HttpPost]
        public async Task<ActionResult<ArtiestDto>> PostArtiest(ArtiestDto artiestDto)
        {
            var artiest = _mapper.Map<Artiest>(artiestDto);
            _context.Artiest.Add(artiest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtiest), new { id = artiest.ArtiestId }, _mapper.Map<ArtiestDto>(artiest));
        }

        // PUT: api/Artiests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtiest(Guid id, ArtiestDto artiestDto)
        {
            if (id != artiestDto.ArtiestId)
            {
                return BadRequest();
            }

            var artiest = _mapper.Map<Artiest>(artiestDto);
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
            return _context.Artiest.Any(e => e.ArtiestId == id);
        }
    }
}
