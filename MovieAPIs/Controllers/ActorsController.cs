using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieData;
using MovieDomain;

namespace MovieAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _config;
        public ActorsController(MovieContext config)
        {
            _config = config;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorsDTO>>> GetActors()
        {
            return await _config.Actors
                .Select(a => new ActorsDTO
                {
                    ActorId = a.ActorId,
                    ActorName = a.ActorName
                }).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorsDTO>> GetActor(int id)
        {
            var actor = await _config.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return new ActorsDTO { ActorId = actor.ActorId,ActorName = actor.ActorName };
        }
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(ActorsDTO actorsDTO)
        {
            if (actorsDTO == null)
            {
                return BadRequest();
            }
            var actor = new Actor
            {
                ActorId = actorsDTO.ActorId,
                ActorName = actorsDTO.ActorName
            };
            _config.Actors.Add(actor);
            await _config.SaveChangesAsync();
            return CreatedAtAction("GetActor", new { id = actor.ActorId },actor);
        }
    }
}
