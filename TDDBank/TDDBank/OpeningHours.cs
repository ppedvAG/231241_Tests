using Microsoft.Data.SqlClient;

namespace TDDBank
{
    public class OpeningHours
    {
        public bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Sunday ||
                   DateTime.Now.DayOfWeek == DayOfWeek.Saturday;
        }

        public int GetCountEmployeesInDB()
        {
            var con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Trusted_Connection=true;Database=PRODIVLJHFKJHBNDKJNDKJN");
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Employees";
            return (int)cmd.ExecuteScalar();
        }

        public bool IsOpen(DateTime dateTime)
        {
            if (dateTime.Day == 24 &&
                dateTime.Month == 12)
                return false;

            // Check if it's Monday and the time is between 10:30 and 19:00
            if (dateTime.DayOfWeek == DayOfWeek.Monday &&
                dateTime.TimeOfDay >= new TimeSpan(10, 30, 0) &&
                dateTime.TimeOfDay < new TimeSpan(19, 0, 0))
            {
                return true;
            }

            // Check if it's Saturday and the time is between 13:00 and 16:00
            if (dateTime.DayOfWeek == DayOfWeek.Saturday &&
                dateTime.TimeOfDay >= new TimeSpan(13, 0, 0) &&
                dateTime.TimeOfDay < new TimeSpan(16, 0, 0))
            {
                return true;
            }

            // No other opening hours specified, default to closed
            return false;
        }

    }
}
