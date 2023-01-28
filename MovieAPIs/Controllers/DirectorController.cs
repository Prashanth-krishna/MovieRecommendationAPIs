using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieData;
using MovieDomain;

namespace MovieAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly MovieContext _context;
        public DirectorController(MovieContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorDTO>>> GetDirectors()
        {
            return await _context.Directors
                .Select(d => new DirectorDTO
                {
                    DirectorId = d.DirectorId,
                    DirectorName = d.DirectorName
                }).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDTO>> GetDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if(director == null)
            {
                return NotFound();
            }
            return  new DirectorDTO
            {
                    DirectorId = director.DirectorId,
                    DirectorName = director.DirectorName
            };
        }
        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(DirectorDTO directorDTO)
        {
            if (directorDTO == null)
            {
                return BadRequest();
            }
            var director = new Director
            {
                DirectorId = directorDTO.DirectorId,
                DirectorName = directorDTO.DirectorName,
            };
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDirector",new { id= director.DirectorId },director);

        }
    }
}
