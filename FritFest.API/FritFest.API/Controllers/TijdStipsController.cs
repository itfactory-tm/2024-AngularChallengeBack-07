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
    public class TijdStipsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public TijdStipsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TijdStips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TijdStipDto>>> GetTijdStip()
        {
            var tijdStips = await _context.TijdStip.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TijdStipDto>>(tijdStips));
        }

        // GET: api/TijdStips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TijdStipDto>> GetTijdStip(Guid id)
        {
            var tijdStip = await _context.TijdStip.FindAsync(id);

            if (tijdStip == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TijdStipDto>(tijdStip));
        }

        // PUT: api/TijdStips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTijdStip(Guid id, TijdStipDto tijdStipDto)
        {
            if (id != tijdStipDto.ArtiestId)
            {
                return BadRequest();
            }

            var tijdStip = _mapper.Map<TijdStip>(tijdStipDto);
            _context.Entry(tijdStip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TijdStipExists(id))
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

        // POST: api/TijdStips
        [HttpPost]
        public async Task<ActionResult<TijdStipDto>> PostTijdStip(TijdStipDto tijdStipDto)
        {
            var tijdStip = _mapper.Map<TijdStip>(tijdStipDto);
            _context.TijdStip.Add(tijdStip);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TijdStipExists(tijdStip.ArtiestId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTijdStip", new { id = tijdStip.ArtiestId }, _mapper.Map<TijdStipDto>(tijdStip));
        }

        // DELETE: api/TijdStips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTijdStip(Guid id)
        {
            var tijdStip = await _context.TijdStip.FindAsync(id);
            if (tijdStip == null)
            {
                return NotFound();
            }

            _context.TijdStip.Remove(tijdStip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TijdStipExists(Guid id)
        {
            return _context.TijdStip.Any(e => e.ArtiestId == id);
        }
    }
}
