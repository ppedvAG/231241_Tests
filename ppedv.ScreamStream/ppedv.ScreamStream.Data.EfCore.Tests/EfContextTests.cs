using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ppedv.ScreamStream.Model;
using System.Reflection;

namespace ppedv.ScreamStream.Data.EfCore.Tests
{
    public class EfContextTests
    {
        readonly string conString = @"Server=(localdb)\mssqllocaldb;Database=ScreamStreamDb_dev;Trusted_Connection=true;";

        [Fact]
        public void Can_create_db()
        {
            var con = new EfContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
            //con.Database.EnsureDeleted();
        }

        [Fact]
        public void Can_add_movie()
        {
            var con = new EfContext(conString);
            var mv = new Movie() { Titel = "Pumpkin Patch Panics" };

            con.Add(mv);
            var result = con.SaveChanges();

            Assert.Equal(1, result);
        }

        [Fact]
        public void Can_read_movie()
        {
            var mv = new Movie() { Titel = $"The Algorithm of Terror_{Guid.NewGuid()}" };
            using (var con = new EfContext(conString))
            {
                con.Add(mv);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Movies.Find(mv.Id);
                Assert.Equal(mv.Titel, loaded?.Titel);
            }
        }

        [Fact]
        public void Can_update_movie()
        {
            var mv = new Movie() { Titel = $"The Debugging of Doom_{Guid.NewGuid()}" };
            var newTitle = $"Cryptic Coding Catastrophe_{Guid.NewGuid()}";
            using (var con = new EfContext(conString))
            {
                con.Add(mv);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Movies.Find(mv.Id);
                loaded.Titel = newTitle;
                var result = con.SaveChanges();
                Assert.Equal(1, result);
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Movies.Find(mv.Id);
                Assert.Equal(newTitle, loaded?.Titel);
            }
        }

        [Fact]
        public void Can_delete_movie()
        {
            var mv = new Movie() { Titel = $"The Specter of Async Await_{Guid.NewGuid()}" };
            using (var con = new EfContext(conString))
            {
                con.Add(mv);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Movies.Find(mv.Id);
                con.Remove(loaded);
                var result = con.SaveChanges();
                Assert.Equal(1, result);
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Movies.Find(mv.Id);
                Assert.Null(loaded);
            }
        }

        [Fact]
        public void Can_read_Movie_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            var mv = fix.Create<Movie>();

            using (var con = new EfContext(conString))
            {
                con.Add(mv);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Movies.Include(x => x.Actors).Include(x => x.Directors).FirstOrDefault(x => x.Id == mv.Id);
                loaded.Should().BeEquivalentTo(mv, x => x.IgnoringCyclicReferences());
            }
        }
    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}