using AutoMapper;
using FritFest.API.DbContexts;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ArticlesController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public ArticlesController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Articles
        [HttpGet]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles()
        {
            var articles = await _context.Article
                .Include(a => a.Edition)  // Include Editie to map Editie.Titel
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ArticleDto>>(articles));
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<ArticleDto>> GetArticle(Guid id)
        {
            var article = await _context.Article
                .Include(a => a.Edition)  // Include Editie to map Editie.Titel
                .FirstOrDefaultAsync(a => a.ArticleId == id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArticleDto>(article));
        }

        // POST: api/Articles
        [HttpPost]
        [Authorize(Policy = "GetAccess")]

        public async Task<ActionResult<ArticleDto>> PostArticle(ArticleDto articleDto)
        {
            var article = _mapper.Map<Article>(articleDto);
            article.ArticleId = Guid.NewGuid();
            _context.Article.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = article.ArticleId }, _mapper.Map<ArticleDto>(article));
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutArticle(Guid id, ArticleDto articleDto)
        {
            if (id != articleDto.ArticleId)
            {
                return BadRequest();
            }

            var artikel = _mapper.Map<Article>(articleDto);
            _context.Entry(artikel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(Guid id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }
    }
}
