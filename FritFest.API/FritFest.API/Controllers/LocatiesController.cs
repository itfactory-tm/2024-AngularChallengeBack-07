using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FritFest.API.DbContexts;
using FritFest.API.Entities;
using FritFest.API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatiesController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public LocatiesController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Locaties
        [HttpGet]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<LocatieDto>>> GetLocatie()
        {
            var locaties = await _context.Locatie
                .Include(l => l.FoodTrucks)
                .Include(l => l.Podia)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<LocatieDto>>(locaties));
        }

        // GET: api/Locaties/5
        [HttpGet("{id}")]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<LocatieDto>> GetLocatie(Guid id)
        {
            var locatie = await _context.Locatie
                .Include(l => l.FoodTrucks)
                .Include(l => l.Podia)
                .FirstOrDefaultAsync(l => l.LocatieId == id);

            if (locatie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LocatieDto>(locatie));
        }

        // PUT: api/Locaties/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutLocatie(Guid id, LocatieDto locatieDto)
        {
            if (id != locatieDto.LocatieId)
            {
                return BadRequest();
            }

            var locatie = _mapper.Map<Locatie>(locatieDto);
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
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<LocatieDto>> PostLocatie(LocatieDto locatieDto)
        {
            var locatie = _mapper.Map<Locatie>(locatieDto);
            locatie.LocatieId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Locatie.Add(locatie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocatie), new { id = locatie.LocatieId }, _mapper.Map<LocatieDto>(locatie));
        }

        // DELETE: api/Locaties/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
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
