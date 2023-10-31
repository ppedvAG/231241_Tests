namespace ppedv.ScreamStream.Model.DomainModel
{
    public class Movie : Entity
    {
        public string Titel { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int UsAgeRating { get; set; }
        public virtual ICollection<Person> Directors { get; set; } = new HashSet<Person>();
        public virtual ICollection<Person> Actors { get; set; } = new HashSet<Person>();
    }
}
