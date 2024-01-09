using Microsoft.EntityFrameworkCore;
using ppedv.PizzaPizzaPizza.Model;

namespace ppedv.PizzaPizzaPizza.Data.EfCore
{
    public class PizzaContext : DbContext
    {
        string conString;

        public PizzaContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Belag> Belaege { get; set; }
    }
}
