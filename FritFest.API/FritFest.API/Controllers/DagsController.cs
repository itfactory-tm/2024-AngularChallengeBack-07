using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FritFest.API.DbContexts;
using FritFest.API.Entities;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DagsController : ControllerBase
    {
        private readonly FestivalContext _context;

        public DagsController(FestivalContext context)
        {
            _context = context;
        }

        // GET: api/Dags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dag>>> GetDag()
        {
            return await _context.Dag.ToListAsync();
        }

        // GET: api/Dags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dag>> GetDag(Guid id)
        {
            var dag = await _context.Dag.FindAsync(id);

            if (dag == null)
            {
                return NotFound();
            }

            return dag;
        }

        // PUT: api/Dags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDag(Guid id, Dag dag)
        {
            if (id != dag.DagId)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<Dag>> PostDag(Dag dag)
        {
            _context.Dag.Add(dag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDag", new { id = dag.DagId }, dag);
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
