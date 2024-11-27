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
    public class FotosController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public FotosController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Fotos
        [HttpGet]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<FotoDto>>> GetFoto()
        {
            var fotos = await _context.Foto
                .Include(f => f.Editie)
                .Include(f => f.Artikel)
                .Include(f => f.Podium)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<FotoDto>>(fotos));
        }

        // GET: api/Fotos/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<FotoDto>> GetFoto(Guid id)
        {
            var foto = await _context.Foto
                .Include(f => f.Editie)
                .Include(f => f.Artikel)
                .Include(f => f.Podium)
                .FirstOrDefaultAsync(f => f.FotoId == id);

            if (foto == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FotoDto>(foto));
        }

        // PUT: api/Fotos/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutFoto(Guid id, FotoDto fotoDto)
        {
            if (id != fotoDto.FotoId)
            {
                return BadRequest();
            }

            var foto = _mapper.Map<Foto>(fotoDto);
            _context.Entry(foto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoExists(id))
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

        // POST: api/Fotos
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<FotoDto>> PostFoto(FotoDto fotoDto)
        {
            var foto = _mapper.Map<Foto>(fotoDto);
            foto.FotoId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Foto.Add(foto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoto), new { id = foto.FotoId }, _mapper.Map<FotoDto>(foto));
        }

        // DELETE: api/Fotos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFoto(Guid id)
        {
            var foto = await _context.Foto.FindAsync(id);
            if (foto == null)
            {
                return NotFound();
            }

            _context.Foto.Remove(foto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotoExists(Guid id)
        {
            return _context.Foto.Any(e => e.FotoId == id);
        }
    }
}
