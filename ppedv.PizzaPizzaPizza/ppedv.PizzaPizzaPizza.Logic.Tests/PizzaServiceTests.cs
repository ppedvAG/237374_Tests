using FluentAssertions;
using ppedv.PizzaPizzaPizza.Model;

namespace ppedv.PizzaPizzaPizza.Logic.Tests
{
    public class PizzaServiceTests
    {
        [Fact]
        public void CalcKCal_pizza_null_throws_ArgumentNullEx()
        {
            var ps = new PizzaService();

            var act = () => ps.CalcKCal(null!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalcKCal_pizza_with_no_Belaege_returns_0()
        {
            var ps = new PizzaService();
            var p = new Pizza();

            var result = ps.CalcKCal(p);

            result.Should().Be(0);
        }

        [Fact]
        public void CalcKCal_pizza_with_1_Belaege_returns_12()
        {
            var ps = new PizzaService();
            var p = new Pizza();
            p.Belaege.Add(new Belag() { KCal = 12 });

            var result = ps.CalcKCal(p);

            result.Should().Be(12);
        }

        [Fact]
        public void CalcKCal_pizza_with_2_Belaege_returns_50()
        {
            var ps = new PizzaService();
            var p = new Pizza();
            p.Belaege.Add(new Belag() { KCal = 12 });
            p.Belaege.Add(new Belag() { KCal = 38 });

            var result = ps.CalcKCal(p);

            result.Should().Be(50);
        }
    }
}