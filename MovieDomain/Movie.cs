using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain
{
    public class Movie
    {
        public Movie()
        {
            Actors = new List<Actor>();
        }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public double Rating { get; set; }
        public Director Director { get; set; }
        public int DirectorId { get; set; }
        public List<Actor> Actors { get; set; }
        public string Genre1 { get; set; }
        public string Genre2 { get; set; }
    }
}
