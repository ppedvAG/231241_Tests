using ppedv.ScreamStream.Model.Contracts;
using ppedv.ScreamStream.Model.DomainModel;

namespace ppedv.ScreamStream.Core
{
    public class MovieService
    {
        private readonly IRepository repo;

        public MovieService(IRepository repo)
        {
            this.repo = repo;
        }
        public Movie GetMovieWithOldestActorsAverageAge()
        {
            var allMovies = repo.Query<Movie>(); // Annahme: repo.Query<T>() gibt alle Filme zurück

            var moviesWithAge = allMovies
                .Select(movie => new
                {
                    Movie = movie,
                    AverageAge = movie.Actors.Any() ?
                        movie.Actors.Select(actor => (DateTime.Today - actor.BirthDate).TotalDays / 365.25).Average() :
                        0.0 // Default-Wert, falls keine Schauspieler vorhanden sind
                })
                .OrderByDescending(m => m.AverageAge)
                .ThenByDescending(m => m.Movie.PublishedDate);

            return moviesWithAge.FirstOrDefault()?.Movie;
        }

    }
}
