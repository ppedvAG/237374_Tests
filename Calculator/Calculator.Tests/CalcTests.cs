namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Sum_2_and_3_results_5()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Sum_0_and_0_results_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Sum_MAX_and_1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }


        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(2, 5, 7)]
        [InlineData(-3, -6, -9)]
        [InlineData(-3, 5, 2)]
        [InlineData(3, -5, -2)]
        public void Sum_results_ok(int a, int b, int exp)
        {
            var calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.Equal(exp, result);
        }

        [Theory]
        [InlineData(int.MinValue, -1)]
        [InlineData(int.MaxValue, 1)]
        public void Sum_throws_OverflowsEx(int a, int b)
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(a, b));
        }
    }
}