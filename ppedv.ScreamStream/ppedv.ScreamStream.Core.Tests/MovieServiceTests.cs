using NSubstitute;
using ppedv.ScreamStream.Model.Contracts;
using ppedv.ScreamStream.Model.DomainModel;

namespace ppedv.ScreamStream.Core.Tests
{
    public class MovieServiceTests
    {
        [Fact]
        public void GetMovieWithOldestActors_empty_db_should_null()
        {
            var repo = Substitute.For<IRepository>();
            var ms = new MovieService(repo);

            var result = ms.GetMovieWithOldestActorsAverageAge();

            Assert.Null(result);
            repo.Received(1).Query<Movie>();
        }

        [Fact]
        public void GetMovieWithOldestActors_3_movies()
        {
            var ms = new MovieService(new TestRepo());

            var result = ms.GetMovieWithOldestActorsAverageAge();

            Assert.Equal("Movie 2", result.Titel);
        }

        [Fact]
        public void GetMovieWithOldestActors_3_movies_nsub()
        {
            var m1 = new Movie() { Titel = "Movie 1" };
            m1.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-10) });
            var m2 = new Movie() { Titel = "Movie 2" };
            m2.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-20) });
            var m3 = new Movie() { Titel = "Movie 3" };
            m3.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-15) });
            var movies = new[] { m1, m2, m3 };
            var repo = Substitute.For<IRepository>();
            repo.Query<Movie>().Returns(movies.AsQueryable());
            var ms = new MovieService(repo);

            var result = ms.GetMovieWithOldestActorsAverageAge();

            Assert.Equal("Movie 2", result.Titel);
            repo.Received(1).Query<Movie>();
            repo.DidNotReceive().Query<Person>();
        }

        [Fact]
        public void GetMovieWithOldestActors_3_movies_with_same_age_sum_then_order_by_published_date()
        {

            var m1 = new Movie() { Titel = "Movie 1", PublishedDate = DateTime.Today.AddDays(-100) };
            m1.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-10) });
            var m2 = new Movie() { Titel = "Movie 2", PublishedDate = DateTime.Today.AddDays(-10) };
            m2.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-10) });
            var m3 = new Movie() { Titel = "Movie 3", PublishedDate = DateTime.Today.AddDays(-50) };
            m3.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-10) });
            var movies = new[] { m1, m2, m3 };
            var repo = Substitute.For<IRepository>();
            repo.Query<Movie>().Returns(movies.AsQueryable());
            var ms = new MovieService(repo);

            var result = ms.GetMovieWithOldestActorsAverageAge();

            Assert.Equal("Movie 2", result.Titel);
            repo.Received(1).Query<Movie>();
            repo.DidNotReceive().Query<Person>();
        }
    }

    public class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            if (typeof(T).IsAssignableFrom(typeof(Movie)))
            {
                var m1 = new Movie() { Titel = "Movie 1" };
                m1.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-10) });
                var m2 = new Movie() { Titel = "Movie 2" };
                m2.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-20) });
                var m3 = new Movie() { Titel = "Movie 3" };
                m3.Actors.Add(new Person() { BirthDate = DateTime.Today.AddYears(-15) });

                return new[] { m1, m2, m3 }.AsQueryable().Cast<T>();
            }
            throw new NotImplementedException();
        }

        public int SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}
