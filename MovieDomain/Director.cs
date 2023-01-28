using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain
{
    public class Director
    {
        public Director()
        {
            Movies = new List<Movie>();
        }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
