namespace MovieDomain
{
    public class Actor
    {
        public Actor()
        {
            Movies = new List<Movie>();
        }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public List<Movie> Movies { get; set; }
    }
}