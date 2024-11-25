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

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodiumsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public PodiumsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Podiums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PodiumDto>>> GetPodium()
        {
            var podiums = await _context.Podium
                .Include(p => p.TijdStippen)
                .Include(p => p.Fotos)
                .Include(p => p.Locatie)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PodiumDto>>(podiums));
        }

        // GET: api/Podiums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PodiumDto>> GetPodium(Guid id)
        {
            var podium = await _context.Podium.FindAsync(id);

            if (podium == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PodiumDto>(podium));
        }

        // PUT: api/Podiums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPodium(Guid id, PodiumDto podiumDto)
        {
            if (id != podiumDto.PodiumId)
            {
                return BadRequest();
            }

            var podium = _mapper.Map<Podium>(podiumDto);
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
        [HttpPost]
        public async Task<ActionResult<PodiumDto>> PostPodium(PodiumDto podiumDto)
        {
            var podium = _mapper.Map<Podium>(podiumDto);
            podium.PodiumId = Guid.NewGuid();
            _context.Podium.Add(podium);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPodium), new { id = podium.PodiumId }, _mapper.Map<PodiumDto>(podium));
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
