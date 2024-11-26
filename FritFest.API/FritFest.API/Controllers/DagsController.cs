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
    public class DagsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public DagsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Dags
        [HttpGet]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<DagDto>>> GetDag()
        {
            var dags = await _context.Dag.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<DagDto>>(dags));
        }

        // GET: api/Dags/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<DagDto>> GetDag(Guid id)
        {
            var dag = await _context.Dag.FindAsync(id);

            if (dag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DagDto>(dag));
        }

        // PUT: api/Dags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutDag(Guid id, DagDto dagDto)
        {
            if (id != dagDto.DagId)
            {
                return BadRequest();
            }

            var dag = _mapper.Map<Dag>(dagDto);
            _context.Entry(dag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DagExists(id))
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

        // POST: api/Dags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DagDto>> PostDag(DagDto dagDto)
        {
            var dag = _mapper.Map<Dag>(dagDto);
            dag.DagId = Guid.NewGuid();
            _context.Dag.Add(dag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDag), new { id = dag.DagId }, _mapper.Map<DagDto>(dag));
        }

        // DELETE: api/Dags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDag(Guid id)
        {
            var dag = await _context.Dag.FindAsync(id);
            if (dag == null)
            {
                return NotFound();
            }

            _context.Dag.Remove(dag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DagExists(Guid id)
        {
            return _context.Dag.Any(e => e.DagId == id);
        }
    }
}
