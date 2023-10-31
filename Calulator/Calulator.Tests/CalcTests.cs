using FluentAssertions;

namespace Calulator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Sum_4_and_5_results_9()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(4, 5);

            //Assert
            Assert.Equal(9, result);
            
        }

        [Fact]
        public void Sum_n4_and_n5_results_n9()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(-4, -5);

            //Assert
            Assert.Equal(-9, result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(4, 5, 9)]
        [InlineData(-4, -5, -9)]
        public void Sum_with_results(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.Equal(exp, result);
        }


        [Fact]
        public void Sum_MAX_and_1_throws_OverflowEx()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }


        [Fact]
        public void Minus_10_and_3_results_7()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Minus(10, 3);

            //Fleunt Assert
            result.Should().Be(7);
            result.Should().BeInRange(2, 7);
        }

        [Theory]
        [InlineData(int.MinValue, 1)]
        [InlineData(int.MaxValue, -1)]
        public void Minus_should_throw_OverflowExceptions(int a, int b)
        {
            var calc = new Calc();

            var act = () => calc.Minus(a, b);

            act.Should().Throw<OverflowException>();
        }

    }
}