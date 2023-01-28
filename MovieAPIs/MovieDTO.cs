using MovieDomain;

namespace MovieAPIs
{
    public class MovieDTO
    {
        public MovieDTO() {
            Actors = new List<ActorDTO>();
        
        }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public double Rating { get; set; }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; }
        public List<ActorDTO> Actors { get; set; }
        public string Genre1 { get; set; }
        public string Genre2 { get; set; }
    }
    public class ActorDTO
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }
    }
}
