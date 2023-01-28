using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieData;
using MovieDomain;
using NuGet.ProjectModel;

namespace MovieAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieContext _context;
        public MovieController(MovieContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovie()
        {
            return await _context.Movies
                .Select(m => new MovieDTO
                {
                    MovieId = m.MovieId,
                    MovieName = m.MovieName,
                    DirectorId = m.DirectorId,
                    DirectorName = (m.Director.DirectorName),
                    Actors = (m.Actors
                    .Select(a => new ActorDTO
                    {
                        ActorId = a.ActorId,
                        ActorName = a.ActorName,
                    })
                    .ToList()),
                    Rating = m.Rating,
                    Genre1 = m.Genre1,
                    Genre2 = m.Genre2,

                }).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(a => a.Actors)
                .Select(a => new MovieDTO
                {
                    MovieId = a.MovieId,
                    MovieName = a.MovieName,
                    DirectorId = a.DirectorId,
                    DirectorName = a.Director.DirectorName,
                    Actors = a.Actors.Select(m => new ActorDTO
                    {
                        ActorId = m.ActorId,
                        ActorName = m.ActorName,
                    }).ToList(),
                    Rating = a.Rating,
                    Genre1 = a.Genre1,
                    Genre2 = a.Genre2,

                })
                .FirstOrDefaultAsync(a => a.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDTO movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }
            var movietoAdd = new Movie
            {
                MovieId = movie.MovieId,
                MovieName = movie.MovieName,
                DirectorId = movie.DirectorId,
                Rating = movie.Rating,
                Genre1 = movie.Genre1,
                Genre2 = movie.Genre2
            };
            _context.Movies.Add(movietoAdd);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovie", new { id = movietoAdd.MovieId }, movietoAdd);
        }
        [HttpGet]
        [Route("GetMovieByGenre")]
        public async Task<ActionResult<List<MovieDTO>>> GetMovieByGenre(string genre1, string genre2)
        {
            if(genre1 != null && genre2 != null)
            {
                return await _context.Movies
                .Where(a => a.Genre1 == genre1 || a.Genre1 == genre2 || a.Genre2 == genre1 || a.Genre2 == genre2)
                .Select(a => new MovieDTO
                {
                    MovieId = a.MovieId,
                    MovieName = a.MovieName,
                    DirectorId = a.DirectorId,
                    Rating = a.Rating,
                    Genre1 = a.Genre1,
                    Genre2 = a.Genre2,

                })
                .ToListAsync();
            }
            else
            {
                return BadRequest();
            }
            
        }
    }
}
