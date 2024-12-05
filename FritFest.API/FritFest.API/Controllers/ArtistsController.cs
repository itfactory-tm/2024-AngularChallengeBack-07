using AutoMapper;
using FritFest.API.DbContexts;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using FritFest.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ArtistsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public ArtistsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Artiests
        [HttpGet]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists()
        {
            var artists = await _context.Artists
                .Include(a => a.Edition)
                .ToListAsync();


            return Ok(_mapper.Map<IEnumerable<ArtistDto>>(artists));
        }

        // GET: api/Artiests/5
        [HttpGet("{id}")]

        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<ArtistDto>> GetArtists(Guid id)
        {
            // Fetch the artist from the database with related data
            var artist = await _context.Artists
                //.Include(a => a.Genres)  // Include Genres to map 
                //.Include(a => a.Editions)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }



            return Ok(_mapper.Map<ArtistDto>(artist));
        }


        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<ArtistDto>> PostArtist(ArtistDto artistDto)
        {
            var spotifyCodePattern = @"artist\/(.*?)\?";

            // Extract ApiCode from Spotify link if present
            if (!string.IsNullOrEmpty(artistDto.SpotifyLink))
            {
                var match = Regex.Match(artistDto.SpotifyLink, spotifyCodePattern);
                if (match.Success)
                {
                    artistDto.ApiCode = match.Groups[1].Value;
                }
            }

            // Fetch Spotify data if ApiCode exists
            if (!string.IsNullOrEmpty(artistDto.ApiCode))
            {
                try
                {
                    var spotifyService = new SpotifyService();
                    var spotifyDetails = await spotifyService.GetSpotifyArtistDetails(artistDto.ApiCode);

                    var spotifyJson = JsonSerializer.Deserialize<JsonElement>(spotifyDetails);

                    // Enrich DTO with Spotify data
                    artistDto.Name = spotifyJson.GetProperty("name").GetString();
                    artistDto.SpotifyLink = spotifyJson.GetProperty("uri").GetString();

                    if (spotifyJson.TryGetProperty("genres", out var genresProperty))
                    {
                        var genres = genresProperty.EnumerateArray().Select(g => g.GetString()).ToList();
                        artistDto.Genre = string.Join(",", genres);
                    }

                    var images = spotifyJson.GetProperty("images");
                    if (images.GetArrayLength() > 0)
                    {
                        artistDto.SpotifyPhoto = images[0].GetProperty("url").GetString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch Spotify data for ApiCode {artistDto.ApiCode}. Error: {ex.Message}");
                }
            }

            // Map DTO to entity and save to database
            var artiest = _mapper.Map<Artist>(artistDto);
            artiest.ArtistId = Guid.NewGuid();
            _context.Artists.Add(artiest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtists), new { id = artiest.ArtistId }, _mapper.Map<ArtistDto>(artiest));
        }

        // PUT: api/Artiests/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutArtist(Guid id, ArtistDto artistDto)
        {
            if (id != artistDto.ArtistId)
            {
                return BadRequest();
            }

            // Fetch existing artist from the database
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            //// Check if the SpotifyLink is different
            if (!string.IsNullOrEmpty(artistDto.SpotifyLink) && artistDto.SpotifyLink != artist.SpotifyLink)
            {
                // Extract ApiCode from the new SpotifyLink
                var spotifyCodePattern = @"artist\/(.*?)\?";
                var match = Regex.Match(artistDto.SpotifyLink, spotifyCodePattern);
                if (match.Success)
                {
                    artistDto.ApiCode = match.Groups[1].Value;
                }

                // Fetch Spotify data if ApiCode exists
                if (!string.IsNullOrEmpty(artistDto.ApiCode))
                {
                    try
                    {
                        var spotifyService = new SpotifyService();
                        var spotifyDetails = await spotifyService.GetSpotifyArtistDetails(artistDto.ApiCode);

                        var spotifyJson = JsonSerializer.Deserialize<JsonElement>(spotifyDetails);

                        // Update entity with Spotify data
                        artistDto.Name = spotifyJson.GetProperty("name").GetString();
                        artistDto.SpotifyLink = spotifyJson.GetProperty("uri").GetString();

                        if (spotifyJson.TryGetProperty("genres", out var genresProperty))
                        {
                            var genres = genresProperty.EnumerateArray().Select(g => g.GetString()).ToList();
                            artistDto.Genre = string.Join(",", genres);
                        }

                        var images = spotifyJson.GetProperty("images");
                        if (images.GetArrayLength() > 0)
                        {
                            artistDto.SpotifyPhoto = images[0].GetProperty("url").GetString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to fetch Spotify data for ApiCode {artistDto.ApiCode}. Error: {ex.Message}");
                    }
                }
            }

            // Update other properties (if necessary) but avoid overwriting existing Spotify data unless required
            _mapper.Map(artistDto, artist);

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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


        // DELETE: api/Artiests/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            var artiest = await _context.Artists.FindAsync(id);
            if (artiest == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artiest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(Guid id)
        {
            return _context.Artists.Any(e => e.ArtistId == id);
        }
    }
}
