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
    public class EditionController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public EditionController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Editie
        [HttpGet]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<EditionDto>>> GetEditions()
        {
            var editions = await _context.Editions
                .Include(e => e.Tickets)
                .Include(e => e.Artists)
                .Include(e => e.Articles)
                .Include(e => e.Sponsors)
                .Include(e => e.Foodtrucks)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EditionDto>>(editions));
        }

        // GET: api/Editie/{id}
        [HttpGet("{id}")]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<EditionDto>> GetEdition(Guid id)
        {
            var edition = await _context.Editions
                .Include(e => e.Tickets)
                .Include(e => e.Artists)
                
                .Include(e => e.Articles)
                .Include(e => e.Sponsors)
                .Include(e => e.Foodtrucks)
                .FirstOrDefaultAsync(e => e.EditionId == id);

            if (edition == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EditionDto>(edition));
        }

        // POST: api/Editie
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<EditionDto>> PostEdition(EditionDto editionDto)
        {
            var edition = _mapper.Map<Edition>(editionDto);
            edition.EditionId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Editions.Add(edition);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEdition), new { id = edition.EditionId }, _mapper.Map<EditionDto>(edition));
        }

        // PUT: api/Editie/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutEdition(Guid id, EditionDto editionDto)
        {
            if (!EditionExists(id))
            {
                return BadRequest();
            }

            var edition = _mapper.Map<EditionDto>(editionDto);
            _context.Entry(edition).State = EntityState.Modified;

            try
            {
              await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Editions.Any(e => e.EditionId == id))
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

        // DELETE: api/Editie/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteEdition(Guid id)
        {
            var edition = await _context.Editions.FindAsync(id);
            if (edition == null)
            {
                return NotFound();
            }

            _context.Editions.Remove(edition);
             await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool EditionExists(Guid id)
        {
            return _context.Editions.Any(e => e.EditionId == id);
        }
    }


}
