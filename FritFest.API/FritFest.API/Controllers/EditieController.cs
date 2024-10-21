using AutoMapper;
using FritFest.API.DbContexts;
using FritFest.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FritFest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditieController : ControllerBase
    {
        private readonly FestivalContext _context;
        private readonly IMapper _mapper;

        public EditieController(FestivalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EditieDto>> GetEdities()
        {
            var edities = _context.Editie.ToList();
            return Ok(_mapper.Map<IEnumerable<EditieDto>>(edities));
        }
    }
}
