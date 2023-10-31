using Microsoft.EntityFrameworkCore;
using ppedv.ScreamStream.Model.DomainModel;

namespace ppedv.ScreamStream.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }

        readonly string conString;
        public EfContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasMany(x => x.Directors).WithMany(x => x.AsDirector).UsingEntity("MovieDirectors");
            modelBuilder.Entity<Movie>().HasMany(x => x.Actors).WithMany(x => x.AsActor).UsingEntity("MovieActors");
        }
    }
}
