using Microsoft.QualityTools.Testing.Fakes;

namespace TDDBank.Tests
{
    public class OpeningHoursTests
    {
        [Fact]
        public void IsWeekend_tests()
        {
            var oh = new OpeningHours();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 10, 30);
                Assert.False(oh.IsWeekend());//Mo
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 10, 31);
                Assert.False(oh.IsWeekend());//Di
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 11, 1);
                Assert.False(oh.IsWeekend());//Mi
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 11, 2);
                Assert.False(oh.IsWeekend());//Do
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 11, 3);
                Assert.False(oh.IsWeekend());//Fr
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 11, 4);
                Assert.True(oh.IsWeekend()); //Sa
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 11, 5);
                Assert.True(oh.IsWeekend()); //So
            }
        }

        [Fact]
        public void GetCountEmployeesInDB_Should_return_9()
        {
            using (ShimsContext.Create())
            {
                var oh = new OpeningHours();
                Microsoft.Data.SqlClient.Fakes.ShimSqlConnection.AllInstances.Open = (x) => { };
                Microsoft.Data.SqlClient.Fakes.ShimSqlCommand.AllInstances.ExecuteScalar = (x) => 9;

                var result = oh.GetCountEmployeesInDB();

                Assert.Equal(9, result);
            }
        }



        [Theory]
        [InlineData(2023, 10, 30, 10, 30, true)]//mo
        [InlineData(2023, 10, 30, 10, 29, false)]//mo
        [InlineData(2023, 10, 30, 10, 31, true)] //mo
        [InlineData(2023, 10, 30, 18, 59, true)] //mo
        [InlineData(2023, 10, 30, 19, 00, false)] //mo
        [InlineData(2023, 11, 4, 13, 0, true)] //sa
        [InlineData(2023, 11, 4, 16, 0, false)] //sa
        [InlineData(2023, 11, 5, 20, 0, false)] //so
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool result)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new OpeningHours();

            Assert.Equal(result, oh.IsOpen(dt));
        }


    }
}
