using Microsoft.EntityFrameworkCore;
using MovieDomain;

namespace MovieData
{
    public class MovieContext:DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public MovieContext(DbContextOptions<MovieContext> options):base(options)
        {

        }
    }
}