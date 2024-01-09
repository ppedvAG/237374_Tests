using FluentAssertions;
using Moq;
using ppedv.PizzaPizzaPizza.Model;
using ppedv.PizzaPizzaPizza.Model.Contracts;

namespace ppedv.PizzaPizzaPizza.Logic.Tests.PizzaServiceTests
{
    public class GetPizzaWithMostKCalTests
    {
        [Fact]
        public void GetPizzaWithMostKCal_no_Pizzas_should_return_null()
        {
            var mock = new Mock<IRepository>();

            var ps = new PizzaService(mock.Object);

            ps.GetPizzaWithMostKCal().Should().BeNull();
        }

        [Fact]
        public void GetPizzaWithMostKCal_3_Pizzas_without_Belag_should_return_null()
        {
            var p1 = new Pizza() { Name = "Pizza Salami" };
            var p2 = new Pizza() { Name = "Pizza Extra Käse" };
            var p3 = new Pizza() { Name = "Pizza Hawaii" };

            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Pizza>()).Returns(() =>
            {
                return new[] { p1, p2, p3 };
            });

            var ps = new PizzaService(mock.Object);

            ps.GetPizzaWithMostKCal().Should().BeNull();
            mock.Verify(x => x.GetAll<Pizza>(), Times.Once());
            mock.Verify(x => x.SaveAll(), Times.Never());
        }

        [Fact]
        public void GetPizzaWithMostKCal_3_Pizzas_same_KCal_same_Belag_count_result_should_be_P2_with_Name()
        {
            var p1 = new Pizza() { Name = "Pizza Salami" };
            p1.Belaege.Add(new Belag() { Name = "Käse", KCal = 200 });

            var p2 = new Pizza() { Name = "Pizza Extra Käse" };
            p2.Belaege.Add(new Belag() { Name = "Käse", KCal = 200 });

            var p3 = new Pizza() { Name = "Pizza Hawaii" };
            p3.Belaege.Add(new Belag() { Name = "Käse", KCal = 200 });

            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Pizza>()).Returns(() =>
            {
                return new[] { p1, p2, p3 };
            });

            var ps = new PizzaService(mock.Object);

            var result = ps.GetPizzaWithMostKCal();

            result.Name.Should().Be("Pizza Extra Käse");
            result.Should().Be(p2);
        }

        [Fact]
        public void GetPizzaWithMostKCal_3_Pizzas_same_KCal_result_should_be_P2_with_less_Belag_()
        {
            var p1 = new Pizza() { Name = "Pizza Salami" };
            p1.Belaege.Add(new Belag() { Name = "Käse", KCal = 200 });
            p1.Belaege.Add(new Belag() { Name = "Salami", KCal = 200 });

            var p2 = new Pizza() { Name = "Pizza Extra Käse" };
            p2.Belaege.Add(new Belag() { Name = "Käse", KCal = 400 });

            var p3 = new Pizza() { Name = "Pizza Hawaii" };
            p3.Belaege.Add(new Belag() { Name = "Käse", KCal = 200 });
            p3.Belaege.Add(new Belag() { Name = "Ananas", KCal = 100 });
            p3.Belaege.Add(new Belag() { Name = "Hühnchen", KCal = 100 });

            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Pizza>()).Returns(() =>
            {
                return new[] { p1, p2, p3 };
            });

            var ps = new PizzaService(mock.Object);

            var result = ps.GetPizzaWithMostKCal();

            result.Name.Should().Be("Pizza Extra Käse");
            result.Should().Be(p2);
        }


        [Fact]
        public void GetPizzaWithMostKCal_3_Pizzas_results_should_be_extra_Käse_Moq()
        {
            var p1 = new Pizza() { Name = "Pizza Salami" };
            p1.Belaege.Add(new Belag() { Name = "Käse", KCal = 100 });
            p1.Belaege.Add(new Belag() { Name = "Salami", KCal = 150 });

            var p2 = new Pizza() { Name = "Pizza Extra Käse" };
            p2.Belaege.Add(new Belag() { Name = "Käse", KCal = 100 });
            p2.Belaege.Add(new Belag() { Name = "Mehr Käse", KCal = 300 });

            var p3 = new Pizza() { Name = "Pizza Hawaii" };
            p3.Belaege.Add(new Belag() { Name = "Käse", KCal = 100 });
            p3.Belaege.Add(new Belag() { Name = "Ananas", KCal = 20 });
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Pizza>()).Returns(() =>
            {
                return new[] { p1, p2, p3 };
            });

            var ps = new PizzaService(mock.Object);

            var result = ps.GetPizzaWithMostKCal();

            result.Name.Should().Be("Pizza Extra Käse");
            result.Should().Be(p2);
        }

        [Fact]
        public void GetPizzaWithMostKCal_3_Pizzas_results_should_be_extra_Käse_TestRepo()
        {
            var ps = new PizzaService(new TestRepo());

            var result = ps.GetPizzaWithMostKCal();

            result.Name.Should().Be("Pizza Extra Käse");
        }

        class TestRepo : IRepository
        {
            public void Add<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }

            public void Delete<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> GetAll<T>() where T : Entity
            {
                if (typeof(T).IsAssignableFrom(typeof(Pizza)))
                {
                    var p1 = new Pizza() { Name = "Pizza Salami" };
                    p1.Belaege.Add(new Belag() { Name = "Käse", KCal = 100 });
                    p1.Belaege.Add(new Belag() { Name = "Salami", KCal = 150 });

                    var p2 = new Pizza() { Name = "Pizza Extra Käse" };
                    p2.Belaege.Add(new Belag() { Name = "Käse", KCal = 100 });
                    p2.Belaege.Add(new Belag() { Name = "Mehr Käse", KCal = 300 });

                    var p3 = new Pizza() { Name = "Pizza Hawaii" };
                    p3.Belaege.Add(new Belag() { Name = "Käse", KCal = 100 });
                    p3.Belaege.Add(new Belag() { Name = "Ananas", KCal = 20 });

                    return new[] { p1, p2, p3 }.Cast<T>();
                }

                throw new NotImplementedException();
            }

            public T GetById<T>(int id) where T : Entity
            {
                throw new NotImplementedException();
            }

            public void SaveAll()
            {
                throw new NotImplementedException();
            }

            public void Update<T>(T entity) where T : Entity
            {
                throw new NotImplementedException();
            }
        }
    }
}
