using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FritFest.API.DbContexts;
using FritFest.API.Entities;
using FritFest.API.Dtos;
using AutoMapper;
using Type = FritFest.API.Entities.Type;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TypesController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public TypesController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetType()
        {
            var types = await _context.Type.ToListAsync();
            return Ok(_mapper.Map<List<TypeDto>>(types));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDto>> GetType(Guid id)
        {
            var type = await _context.Type.FindAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TypeDto>(type));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(Guid id, TypeDto typeDto)
        {
            if (id != typeDto.TypeId)
            {
                return BadRequest();
            }

            var type = _mapper.Map<Type>(typeDto);
            _context.Entry(type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
                {
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TypeDto>> PostType(TypeDto typeDto)
        {
            var type = _mapper.Map<Type>(typeDto);
            _context.Type.Add(type);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetType",new{id = type.TypeId}, _mapper.Map<TypeDto>(type));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            var type = await _context.Type.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            _context.Type.Remove(type);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TypeExists(Guid id)
        {
            return _context.Type.Any(e => e.TypeId == id);
        }
    }
}