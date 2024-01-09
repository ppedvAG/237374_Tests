using ppedv.PizzaPizzaPizza.Model;

namespace ppedv.PizzaPizzaPizza.Data.EfCore.Tests
{
    public class PizzaContextTests
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaPizzaPizza_dev;Trusted_Connection=true;";

        [Fact]
        public void Can_create_DB()
        {
            string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaPizzaPizza_createdTest;Trusted_Connection=true;";
            using var con = new PizzaContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
            con.Database.EnsureDeleted();
        }

        [Fact]
        public void Can_add_Pizza()
        {
            var pizza = new Pizza() { Name = "P1", Preis = 5.55m };
            using var con = new PizzaContext(conString);
            con.Database.EnsureCreated();

            con.Add(pizza);
            var rows = con.SaveChanges();

            Assert.Equal(1, rows);
        }

        [Fact]
        public void Can_read_Pizza()
        {
            var pizza = new Pizza() { Name = $"P1_{Guid.NewGuid()}", Preis = 5.55m };
            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(pizza);
                con.SaveChanges();
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                Assert.Equal(pizza.Name, loaded.Name);
            }
        }

        [Fact]
        public void Can_update_Pizza()
        {
            var pizza = new Pizza() { Name = $"P1_{Guid.NewGuid()}", Preis = 5.55m };
            var newName = $"P3_{Guid.NewGuid()}";
            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(pizza);
                con.SaveChanges();
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                loaded.Name = newName;
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                Assert.Equal(newName, loaded.Name);
            }
        }

        [Fact]
        public void Can_delete_Pizza()
        {
            var pizza = new Pizza() { Name = $"PD_{Guid.NewGuid()}", Preis = 5.55m };
            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(pizza);
                con.SaveChanges();
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                con.Remove(loaded);
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                Assert.Null(loaded);
            }
        }
    }
}
