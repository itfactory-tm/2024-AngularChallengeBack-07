using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FritFest.API.DbContexts;
using FritFest.API.Entities;
using AutoMapper;
using FritFest.API.Dtos;
using Humanizer.Localisation;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public UserTypesController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UserTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTypeDto>>> GetUserType()
        {
            var userTypes = await _context.UserType.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserTypeDto>>(userTypes));
        }

        // GET: api/UserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeDto>> GetUserType(Guid id)
        {
            var userType = await _context.UserType.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenreDto>(userType));
        }

        // PUT: api/UserTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType(Guid id, UserTypeDto userTypeDto)
        {
            if (id != userTypeDto.TypeId)
            {
                return BadRequest();
            }

            var userType = _mapper.Map<UserType>(userTypeDto);
            _context.Entry(userType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // POST: api/UserTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserTypeDto>> PostUserType(UserTypeDto userTypeDto)
        {
            var userType = _mapper.Map<UserType>(userTypeDto);
            _context.UserType.Add(userType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserType), new { id = userType.TypeId }, _mapper.Map<GenreDto>(userType));
        }

        // DELETE: api/UserTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(Guid id)
        {
            var userType = await _context.UserType.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.UserType.Remove(userType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTypeExists(Guid id)
        {
            return _context.UserType.Any(e => e.TypeId == id);
        }
    }
}
