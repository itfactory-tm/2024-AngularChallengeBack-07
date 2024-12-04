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
    public class SponsorsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public SponsorsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Sponsors
        [HttpGet]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<SponsorDto>>> GetSponsors()
        {
            var sponsors = await _context.Sponsor
                .Include(s => s.Edition)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<SponsorDto>>(sponsors));
        }

        // GET: api/Sponsors/5
        [HttpGet("{id}")]
        [EnableRateLimiting("PublicLimiter")]

        public async Task<ActionResult<SponsorDto>> GetSponsor(Guid id)
        {
            var sponsor = await _context.Sponsor
                .Include(s => s.Edition)
                .FirstOrDefaultAsync(s => s.SponsorId == id);

            if (sponsor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SponsorDto>(sponsor));
        }

        // PUT: api/Sponsors/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutSponsor(Guid id, SponsorDto sponsorDto)
        {
            if (id != sponsorDto.SponsorId)
            {
                return BadRequest();
            }

            var sponsor = _mapper.Map<Sponsor>(sponsorDto);
            _context.Entry(sponsor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorExists(id))
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

        // POST: api/Sponsors
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<SponsorDto>> PostSponsor(SponsorDto sponsorDto)
        {
            var sponsor = _mapper.Map<Sponsor>(sponsorDto);
            sponsor.SponsorId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.Sponsor.Add(sponsor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSponsor), new { id = sponsor.SponsorId }, _mapper.Map<SponsorDto>(sponsor));
        }

        // DELETE: api/Sponsors/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteSponsor(Guid id)
        {
            var sponsor = await _context.Sponsor.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            _context.Sponsor.Remove(sponsor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SponsorExists(Guid id)
        {
            return _context.Sponsor.Any(e => e.SponsorId == id);
        }
    }
}
