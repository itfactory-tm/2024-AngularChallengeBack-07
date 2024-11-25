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
    public class EditieController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public EditieController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Editie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EditieDto>>> GetEditie()
        {
            var edities = await _context.Editie
                .Include(e => e.Tickets)
                .Include(e => e.Artiesten)
                .Include(e => e.Fotos)
                .Include(e => e.Artikelen)
                .Include(e => e.Sponsors)
                .Include(e => e.Foodtrucks)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EditieDto>>(edities));
        }

        // GET: api/Editie/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EditieDto>> GetEditie(Guid id)
        {
            var editie = await _context.Editie
                .Include(e => e.Tickets)
                .Include(e => e.Artiesten)
                .Include(e => e.Fotos)
                .Include(e => e.Artikelen)
                .Include(e => e.Sponsors)
                .Include(e => e.Foodtrucks)
                .FirstOrDefaultAsync(e => e.EditieId == id);

            if (editie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EditieDto>(editie));
        }

        // POST: api/Editie
        [HttpPost]
        public async Task<ActionResult<EditieDto>> PostEditie(EditieDto editieDto)
        {
            var editie = _mapper.Map<Editie>(editieDto);
            editie.EditieId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Editie.Add(editie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEditie), new { id = editie.EditieId }, _mapper.Map<EditieDto>(editie));
        }

        // PUT: api/Editie/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEditie(Guid id, EditieDto editieDto)
        {
            if (!EditieExists(id))
            {
                return BadRequest();
            }

            var editie = _mapper.Map<EditieDto>(editieDto);
            _context.Entry(editie).State = EntityState.Modified;

            try
            {
              await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Editie.Any(e => e.EditieId == id))
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
        public async Task<IActionResult> DeleteEditie(Guid id)
        {
            var editie = await _context.Editie.FindAsync(id);
            if (editie == null)
            {
                return NotFound();
            }

            _context.Editie.Remove(editie);
             await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool EditieExists(Guid id)
        {
            return _context.Genre.Any(e => e.GenreId == id);
        }
    }


}
