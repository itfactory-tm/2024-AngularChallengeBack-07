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
        public ActionResult<IEnumerable<EditieDto>> GetEdities()
        {
            var edities = _context.Editie.ToList();
            return Ok(_mapper.Map<IEnumerable<EditieDto>>(edities));
        }

        // GET: api/Editie/{id}
        [HttpGet("{id}")]
        public ActionResult<EditieDto> GetEditie(Guid id)
        {
            var editie = _context.Editie.Find(id);

            if (editie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EditieDto>(editie));
        }

        // POST: api/Editie
        [HttpPost]
        public ActionResult<EditieDto> PostEditie(EditieDto editieDto)
        {
            var editie = _mapper.Map<Editie>(editieDto);
            editie.EditieId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Editie.Add(editie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEditie), new { id = editie.EditieId }, _mapper.Map<EditieDto>(editie));
        }

        // PUT: api/Editie/{id}
        [HttpPut("{id}")]
        public IActionResult PutEditie(Guid id, EditieDto editieDto)
        {
            if (id != editieDto.EditieId)
            {
                return BadRequest();
            }

            var editie = _mapper.Map<Editie>(editieDto);
            _context.Entry(editie).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
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
        public IActionResult DeleteEditie(Guid id)
        {
            var editie = _context.Editie.Find(id);
            if (editie == null)
            {
                return NotFound();
            }

            _context.Editie.Remove(editie);
            _context.SaveChanges();

            return NoContent();
        }
    }


}
