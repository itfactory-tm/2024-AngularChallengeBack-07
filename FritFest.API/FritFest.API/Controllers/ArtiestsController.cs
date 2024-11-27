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
    
    public class ArtiestsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public ArtiestsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Artiests
        [HttpGet]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<ArtiestDto>>> GetArtiests()
        {
            var artiesten = await _context.Artiest
                //.Include(a => a.Genre)
                .Include(a => a.Editie)
                .ToListAsync();

            var spotifyService = new SpotifyService();
            var artiestDtos = new List<ArtiestDto>();

            foreach (var artiest in artiesten)
            {
                var artiestDto = _mapper.Map<ArtiestDto>(artiest);

                if (!string.IsNullOrEmpty(artiest.ApiCode))
                {
                    try
                    {
                        Console.WriteLine($"Fetching Spotify data for ApiCode: {artiest.ApiCode}");
                        var spotifyDetails = await spotifyService.GetSpotifyArtistDetails(artiest.ApiCode);
                        Console.WriteLine($"Spotify API Response: {spotifyDetails}");

                        var spotifyJson = JsonSerializer.Deserialize<JsonElement>(spotifyDetails);

                        artiestDto.Naam = spotifyJson.GetProperty("name").GetString();
                        
                        
                        if(spotifyJson.TryGetProperty("genres",out var genresProperty))
                        {
                            var genres = genresProperty.EnumerateArray().Select(g => g.GetString()).ToList();
                            artiestDto.Genre = string.Join(",", genres);
                        }

                        var images = spotifyJson.GetProperty("images");
                        if (images.GetArrayLength() > 0)
                        {
                            artiestDto.SpotifyPhoto = images[0].GetProperty("url").GetString();
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to fetch Spotify data for ApiCode {artiest.ApiCode}. Error: {ex.Message}");
                    }
                }

                artiestDtos.Add(artiestDto);
            }

            return Ok(artiestDtos);
        }

        // GET: api/Artiests/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<ArtiestDto>> GetArtiest(Guid id)
        {
            // Fetch the artist from the database with related data
            var artiest = await _context.Artiest
                //.Include(a => a.Genre)  // Include Genre to map 
                .Include(a => a.Editie)
                .FirstOrDefaultAsync(a => a.ArtiestId == id);

            if (artiest == null)
            {
                return NotFound();
            }

            // Map to DTO
            var artiestDto = _mapper.Map<ArtiestDto>(artiest);

            // Fetch additional Spotify data if ApiCode exists
            if (!string.IsNullOrEmpty(artiest.ApiCode))
            {
                try
                {
                    Console.WriteLine($"Fetching Spotify data for ApiCode: {artiest.ApiCode}");
                    var spotifyService = new SpotifyService();
                    var spotifyDetails = await spotifyService.GetSpotifyArtistDetails(artiest.ApiCode);
                    Console.WriteLine($"Spotify API Response: {spotifyDetails}");

                    var spotifyJson = JsonSerializer.Deserialize<JsonElement>(spotifyDetails);

                    // Enrich DTO with Spotify data
                    artiestDto.Naam = spotifyJson.GetProperty("name").GetString();
                   

                    if (spotifyJson.TryGetProperty("genres", out var genresProperty))
                    {
                        var genres = genresProperty.EnumerateArray().Select(g => g.GetString()).ToList();
                        artiestDto.Genre = string.Join(",", genres);
                    }

                    var images = spotifyJson.GetProperty("images");
                    if (images.GetArrayLength() > 0)
                    {
                        artiestDto.SpotifyPhoto = images[0].GetProperty("url").GetString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch Spotify data for ApiCode {artiest.ApiCode}. Error: {ex.Message}");
                }
            }

            return Ok(artiestDto);
        }


        // POST: api/Artiests
        [HttpPost]
        
        public async Task<ActionResult<ArtiestDto>> PostArtiest(ArtiestDto artiestDto)
        {
            var spotifyCodePattern = @"artist\/(.*?)\?";

            if (!string.IsNullOrEmpty(artiestDto.SpotifyLink))
            {
                var match = Regex.Match(artiestDto.SpotifyLink, spotifyCodePattern);
                if (match.Success)
                {
                    artiestDto.ApiCode = match.Groups[1].Value;
                }
            }

            var artiest = _mapper.Map<Artiest>(artiestDto);
            artiest.ArtiestId = Guid.NewGuid();
            _context.Artiest.Add(artiest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtiest), new { id = artiest.ArtiestId }, _mapper.Map<ArtiestDto>(artiest));
        }

        // PUT: api/Artiests/5
        // PUT: api/Artiests/5
        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutArtiest(Guid id, ArtiestDto artiestDto)
        {
            if (id != artiestDto.ArtiestId)
            {
                return BadRequest();
            }

            // Extract the Spotify ApiCode from the SpotifyLink
            var spotifyCodePattern = @"artist\/(.*?)\?";
            if (!string.IsNullOrEmpty(artiestDto.SpotifyLink))
            {
                var match = Regex.Match(artiestDto.SpotifyLink, spotifyCodePattern);
                if (match.Success)
                {
                    artiestDto.ApiCode = match.Groups[1].Value;
                }
            }

            // Fetch additional data from Spotify if ApiCode is present
            if (!string.IsNullOrEmpty(artiestDto.ApiCode))
            {
                try
                {
                    Console.WriteLine($"Fetching Spotify data for ApiCode: {artiestDto.ApiCode}");
                    var spotifyService = new SpotifyService();
                    var spotifyDetails = await spotifyService.GetSpotifyArtistDetails(artiestDto.ApiCode);
                    Console.WriteLine($"Spotify API Response: {spotifyDetails}");

                    var spotifyJson = JsonSerializer.Deserialize<JsonElement>(spotifyDetails);

                    // Update the ArtiestDto with Spotify data
                    artiestDto.Naam = spotifyJson.GetProperty("name").GetString();
                   

                    if (spotifyJson.TryGetProperty("genres", out var genresProperty))
                    {
                        var genres = genresProperty.EnumerateArray().Select(g => g.GetString()).ToList();
                        artiestDto.Genre = string.Join(",", genres);
                    }

                    var images = spotifyJson.GetProperty("images");
                    if (images.GetArrayLength() > 0)
                    {
                        artiestDto.SpotifyPhoto = images[0].GetProperty("url").GetString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch Spotify data for ApiCode {artiestDto.ApiCode}. Error: {ex.Message}");
                }
            }

            // Map the DTO back to the entity and update the database
            var artiest = _mapper.Map<Artiest>(artiestDto);
            _context.Entry(artiest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtiestExists(id))
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
        
        public async Task<IActionResult> DeleteArtiest(Guid id)
        {
            var artiest = await _context.Artiest.FindAsync(id);
            if (artiest == null)
            {
                return NotFound();
            }

            _context.Artiest.Remove(artiest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtiestExists(Guid id)
        {
            return _context.Artiest.Any(e => e.ArtiestId == id);
        }
    }
}
