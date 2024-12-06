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
    public class StagesController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public StagesController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Podiums
        [HttpGet]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<StageDto>>> GetStages()
        {
            var stages = await _context.Stages
                .Include(p => p.TimeSlots)
                .Include(p => p.Photos)
                .Include(p => p.Location)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<StageDto>>(stages));
        }

        // GET: api/Podiums/5
        [HttpGet("{id}")]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<StageDto>> GetStage(Guid id)
        {
            var stage = await _context.Stages
                .Include(p => p.TimeSlots)
                .Include(p => p.Photos)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(s => s.StageId == id);

            if (stage == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StageDto>(stage));
        }

        // PUT: api/Podiums/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutPodium(Guid id, StageDto dto)
        {
            if (id != dto.StageId)
            {
                return BadRequest();
            }

            var podium = _mapper.Map<Stage>(dto);
            _context.Entry(podium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(id))
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
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<StageDto>> PostStage(StageDto dto)
        {
            var stage = _mapper.Map<Stage>(dto);
            stage.StageId = Guid.NewGuid();
            _context.Stages.Add(stage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStage), new { id = stage.StageId }, _mapper.Map<StageDto>(stage));
        }

        // DELETE: api/Podiums/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteStage(Guid id)
        {
            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }

            _context.Stages.Remove(stage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StageExists(Guid id)
        {
            return _context.Stages.Any(e => e.StageId == id);
        }
    }
}
