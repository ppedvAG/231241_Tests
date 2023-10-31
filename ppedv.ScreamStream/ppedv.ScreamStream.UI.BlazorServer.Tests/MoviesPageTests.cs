using Bunit;
using ppedv.ScreamStream.Data.EfCore;
using ppedv.ScreamStream.Model.Contracts;
using ppedv.ScreamStream.UI.BlazorServer.Pages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ppedv.ScreamStream.Model.DomainModel;

namespace ppedv.ScreamStream.UI.BlazorServer.Tests
{

    public class MoviesPageTests : TestContext
    {
        string conString = @"Server=(localdb)\mssqllocaldb;Database=ScreamStreamDb_dev;Trusted_Connection=true;";

        [Fact]
        public void Count_Movie_row_with_db()
        {
            Services.AddSingleton<IRepository>(y => new EfRepository(conString));
            var cut = RenderComponent<Movies>();

            // Assert
            var rows = cut.FindAll("tr"); // Alle 'tr' Elemente in der gerenderten Komponente finden

            Assert.True(rows.Count > 1);
        }

        [Fact]
        public void Count_Movie_row_with_mock()
        {
            var m1 = new Movie() { Titel = "Movie 1" };
            var m2 = new Movie() { Titel = "Movie 2" };
            var m3 = new Movie() { Titel = "Movie 3" };
            var movies = new[] { m1, m2, m3 };
            var repo = Substitute.For<IRepository>();
            repo.Query<Movie>().Returns(movies.AsQueryable());
            Services.AddSingleton(repo);

            var cut = RenderComponent<Movies>();

            // Assert
            var rows = cut.FindAll("tr"); // Alle 'tr' Elemente in der gerenderten Komponente finden
            

            // Erwartete Anzahl von Zeilen
            int expectedRowCount = 3 + 1; // Hier anpassen, je nachdem, wie viele Zeilen Sie erwarten

            Assert.Equal(expectedRowCount, rows.Count);
        }
    }
}