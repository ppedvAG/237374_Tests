using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ppedv.PizzaPizzaPizza.Data.EfCore;
using ppedv.PizzaPizzaPizza.Model;
using ppedv.PizzaPizzaPizza.Model.Contracts;
using ppedv.PizzaPizzaPizza.UI.BlazorWeb.Pages;

namespace ppedv.PizzaPizzaPizza.UI.BlazorWeb.Tests
{
    public class PizzaPageTests : TestContext
    {
        [Fact]
        public void Loading_Page_should_show_all_Pizzas_from_db()
        {
            string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaPizzaPizza_dev;Trusted_Connection=true;";
            Services.AddScoped<IRepository>(x => new EfRepository(conString));

            var cut = RenderComponent<PizzaPage>();

            var rows = cut.FindAll("tr");

            Assert.True(rows.Count > 1);
        }

        [Fact]
        public void Loading_Page_should_show_all_Pizzas()
        {
            var p1 = new Pizza() { Name = "Pizza Salami" };
            var p2 = new Pizza() { Name = "Pizza Extra Käse" };
            var p3 = new Pizza() { Name = "Pizza Hawaii" };

            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Pizza>()).Returns(() =>
            {
                return new[] { p1, p2, p3 };
            });

            Services.AddScoped<IRepository>(x => mock.Object);

            var cut = RenderComponent<PizzaPage>();

            var rows = cut.FindAll("tr");

            Assert.Equal(3 + 1, rows.Count);
        }
    }
}