namespace ppedv.ScreamStream.Core.Tests
{
    public class PeopleServiceTests
    {
        public static readonly object[][] FamousPeopleData =
        {
                new object[] { new DateTime(1879, 3, 14), new DateTime(2023, 10, 30), 144 }, // Albert Einstein
                new object[] { new DateTime(1955, 6, 18), new DateTime(2023, 10, 30), 68 },  // Kevin Costner
                new object[] { new DateTime(1963, 8, 9), new DateTime(2023, 10, 30), 60 },   // Whitney Houston
                new object[] { new DateTime(1981, 12, 8), new DateTime(2023, 10, 30), 41 },  // Ian Somerhalder
        };

        [Theory]
        [MemberData(nameof(FamousPeopleData))]
        public void CalcAge_ReturnsCorrectAgeForFamousPeople(DateTime birthdate, DateTime today, int expectedAge)
        {
            // Arrange
            var ageCalculator = new PeopleService();

            // Act
            var calculatedAge = ageCalculator.CalcAge(birthdate, today);

            // Assert
            Assert.Equal(expectedAge, calculatedAge);
        }
    }
}