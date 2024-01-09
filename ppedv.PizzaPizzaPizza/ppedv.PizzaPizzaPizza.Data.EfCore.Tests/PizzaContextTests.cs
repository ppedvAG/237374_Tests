using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.PizzaPizzaPizza.Model;
using System.Reflection;

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

            result.Should().BeTrue();
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

            rows.Should().Be(1);
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

                loaded.Should().NotBeNull();
                loaded?.Name.Should().Be(pizza.Name);
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
                rows.Should().Be(1);
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                loaded.Should().NotBeNull();
                loaded?.Name.Should().Be(newName);
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
                rows.Should().Be(1);
            }

            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(pizza.Id);
                loaded.Should().BeNull();
            }
        }


        [Fact]
        public void Can_create_and_read_Pizza_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            var p = fix.Create<Pizza>();

            using (var con = new PizzaContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(p);
                con.SaveChanges().Should().BeGreaterThan(6);
            }
            using (var con = new PizzaContext(conString))
            {
                var loaded = con.Pizzas.Find(p.Id);

                loaded.Should().BeEquivalentTo(p, x => x.IgnoringCyclicReferences());
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
}
