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
    }
}