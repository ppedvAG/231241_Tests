// See https://aka.ms/new-console-template for more information

using ppedv.ScreamStream.Model.Contracts;
using ppedv.ScreamStream.Model.DomainModel;

Console.WriteLine("Hello, World!");

string conString = @"Server=(localdb)\mssqllocaldb;Database=ScreamStreamDb_dev;Trusted_Connection=true;";

IRepository repo = new ppedv.ScreamStream.Data.EfCore.EfRepository(conString);

foreach (var m in repo.Query<Movie>().OrderBy(x => x.PublishedDate).ToList())
{
    Console.WriteLine($"{m.Titel} {m.PublishedDate:d} Rating: {m.UsAgeRating}");
    Console.WriteLine("Actors:");
    foreach (var a in m.Actors)
    {
        Console.WriteLine($"\t{a.Name}");
    }
    Console.WriteLine("Directors:");
    foreach (var d in m.Directors)
    {
        Console.WriteLine($"\t{d.Name}");
    }
}