using ppedv.PizzaPizzaPizza.Data.EfCore;
using ppedv.PizzaPizzaPizza.Logic;
using ppedv.PizzaPizzaPizza.Model;
using ppedv.PizzaPizzaPizza.Model.Contracts;
using System.Runtime.InteropServices;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("*** Pizza Pizza Pizza v0.1 ***");

string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaPizzaPizza_dev;Trusted_Connection=true;";

IRepository repo = new EfRepository(conString);
var ps = new PizzaService(repo);

foreach (var p in repo.GetAll<Pizza>())
{
    Console.WriteLine($"{p.Name} {p.Preis:c} KCal: {ps.CalcKCal(p)}");
    foreach (var b in p.Belaege)
    {
        Console.WriteLine($"\t{b.Name} {b.KCal}");
    }
}

var fattestPizza = ps.GetPizzaWithMostKCal();
Console.WriteLine($"Fattest 🍕: {fattestPizza.Name}");
