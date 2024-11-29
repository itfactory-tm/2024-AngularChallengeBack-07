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
    public class MenuItemsController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public MenuItemsController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/MenuItems
        [HttpGet]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetMenuItems()
        {
            var menuItems = await _context.MenuItem
                .Include(mi => mi.FoodTruck)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(menuItems));
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        [EnableRateLimiting("PublicLimiter")]
        public async Task<ActionResult<MenuItemDto>> GetMenuItem(Guid id)
        {
            var menuItem = await _context.MenuItem
                .Include(mi => mi.FoodTruck)
                .FirstOrDefaultAsync(mi => mi.MenuItemId == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MenuItemDto>(menuItem));
        }

        // PUT: api/MenuItems/5
        [HttpPut("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> PutMenuItem(Guid id, MenuItemDto menuItemDto)
        {
            if (id != menuItemDto.MenuItemId)
            {
                return BadRequest();
            }

            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            _context.Entry(menuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
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

        // POST: api/MenuItems
        [HttpPost]
        [Authorize(Policy = "GetAccess")]
        public async Task<ActionResult<MenuItemDto>> PostMenuItem(MenuItemDto menuItemDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            menuItem.MenuItemId = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.MenuItem.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenuItem), new { id = menuItem.MenuItemId }, _mapper.Map<MenuItemDto>(menuItem));
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GetAccess")]
        public async Task<IActionResult> DeleteMenuItem(Guid id)
        {
            var menuItem = await _context.MenuItem.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _context.MenuItem.Remove(menuItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuItemExists(Guid id)
        {
            return _context.MenuItem.Any(e => e.MenuItemId == id);
        }
    }
}
