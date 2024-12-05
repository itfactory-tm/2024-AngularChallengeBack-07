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
            var sponsors = await _context.Sponsors
                .Include(s => s.Edition)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<SponsorDto>>(sponsors));
        }

        // GET: api/Sponsors/5
        [HttpGet("{id}")]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<SponsorDto>> GetSponsor(Guid id)
        {
            var sponsor = await _context.Sponsors
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

            // Handle the SponsorLogo update if Base64 image is provided
            if (!string.IsNullOrEmpty(sponsorDto.SponsorLogoBase64))
            {
                try
                {
                    // Extract the Base64 string (Remove the data URL prefix, if any)
                    var base64String = sponsorDto.SponsorLogoBase64.Replace("data:image/png;base64,", "")
                        .Replace("data:image/jpeg;base64,", "");
                    // Convert to byte array
                    var sponsorLogoBytes = Convert.FromBase64String(base64String);
                    sponsorDto.SponsorLogoBase64 = null; // Ensure no leftover Base64 string in the DTO

                    // Map SponsorDto to Sponsor entity
                    var sponsor = _mapper.Map<Sponsor>(sponsorDto);
                    sponsor.SponsorLogo = sponsorLogoBytes; // Assign the new image bytes to SponsorLogo

                    _context.Entry(sponsor).State = EntityState.Modified;
                }
                catch (FormatException ex)
                {
                    // Return a BadRequest if the Base64 string is invalid
                    return BadRequest("Invalid Base64 string for SponsorLogo: " + ex.Message);
                }
            }
            else
            {
                // If no new image is provided, just map the SponsorDto to the Sponsor entity
                var sponsor = _mapper.Map<Sponsor>(sponsorDto);
                _context.Entry(sponsor).State = EntityState.Modified;
            }

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
            // Check if the SponsorDto contains a Base64 image and convert it to a byte array
            if (!string.IsNullOrEmpty(sponsorDto.SponsorLogoBase64))
            {
                try
                {
                    // Extract the Base64 string (Remove the data URL prefix, if any)
                    var base64String = sponsorDto.SponsorLogoBase64.Replace("data:image/png;base64,", "")
                        .Replace("data:image/jpeg;base64,", "");
                    // Convert to byte array
                    var sponsorLogoBytes = Convert.FromBase64String(base64String);
                    sponsorDto.SponsorLogoBase64 = null; // Ensure no leftover Base64 string in the DTO

                    // Map SponsorDto to Sponsor entity
                    var sponsor = _mapper.Map<Sponsor>(sponsorDto);
                    sponsor.SponsorId = Guid.NewGuid(); // Ensure a new GUID is assigned
                    sponsor.SponsorLogo = sponsorLogoBytes; // Assign the image bytes to the SponsorLogo field

                    // Add the new sponsor to the context
                    _context.Sponsors.Add(sponsor);
                    await _context.SaveChangesAsync();

                    // Return a response with the created sponsor DTO
                    return CreatedAtAction(nameof(GetSponsor), new { id = sponsor.SponsorId },
                        _mapper.Map<SponsorDto>(sponsor));
                }
                catch (FormatException ex)
                {
                    // Return a BadRequest if the Base64 string is invalid
                    return BadRequest("Invalid Base64 string for SponsorLogo: " + ex.Message);
                }
            }
            else
            {
                // Handle case when no SponsorLogoBase64 is provided
                var sponsor = _mapper.Map<Sponsor>(sponsorDto);
                sponsor.SponsorId = Guid.NewGuid(); // Ensure a new GUID is assigned

                // Add the new sponsor to the context
                _context.Sponsors.Add(sponsor);
                await _context.SaveChangesAsync();

                // Return a response with the created sponsor DTO
                return CreatedAtAction(nameof(GetSponsor), new { id = sponsor.SponsorId },
                    _mapper.Map<SponsorDto>(sponsor));
            }
        }

        // DELETE: api/Sponsors/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteSponsor(Guid id)
        {
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            _context.Sponsors.Remove(sponsor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SponsorExists(Guid id)
        {
            return _context.Sponsors.Any(e => e.SponsorId == id);
        }
    }
}