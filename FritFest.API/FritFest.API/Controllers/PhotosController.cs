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
    public class PhotosController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public PhotosController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Fotos
        [HttpGet]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos()
        {
            var photos = await _context.Photos
                .Include(f => f.Edition)
                .Include(f => f.Article)
                .Include(f => f.Stage)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PhotoDto>>(photos));
        }

        // GET: api/Fotos/5
        [HttpGet("{id}")]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<PhotoDto>> GetPhoto(Guid id)
        {
            var photo = await _context.Photos
                .Include(f => f.Edition)
                .Include(f => f.Article)
                .Include(f => f.Stage)
                .FirstOrDefaultAsync(f => f.PhotoId == id);

            if (photo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PhotoDto>(photo));
        }

        // PUT: api/Fotos/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutPhoto(Guid id, PhotoDto photoDto)
        {
            if (id != photoDto.PhotoId)
            {
                return BadRequest();
            }

            var foto = _mapper.Map<Photo>(photoDto);
            _context.Entry(foto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
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
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<PhotoDto>> PostPhoto(PhotoDto photoDto)
        {
            var photo = _mapper.Map<Photo>(photoDto);
            photo.PhotoId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhoto), new { id = photo.PhotoId }, _mapper.Map<PhotoDto>(photo));
        }

        // DELETE: api/Fotos/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeletePhoto(Guid id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(Guid id)
        {
            return _context.Photos.Any(e => e.PhotoId == id);
        }
    }
}
