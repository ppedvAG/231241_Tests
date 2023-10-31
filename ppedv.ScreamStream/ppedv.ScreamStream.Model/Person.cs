namespace ppedv.ScreamStream.Model
{
    public class Person : Entity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Movie> AsDirector { get; set; } = new HashSet<Movie>();
        public virtual ICollection<Movie> AsActor { get; set; } = new HashSet<Movie>();
    }
}
