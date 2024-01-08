namespace TddBank
{
    public class OpeningHours
    {
        // Example opening hours: 10:30 to 19:00 on weekdays, 10:30 to 14:00 on Saturdays, closed on Sundays
        public bool IsOpen(DateTime dateTime)
        {
            DayOfWeek day = dateTime.DayOfWeek;
            TimeSpan time = dateTime.TimeOfDay;

            if (day == DayOfWeek.Sunday)
            {
                return false;
            }
            else if (day == DayOfWeek.Saturday)
            {
                return time >= new TimeSpan(10, 30, 0) && time < new TimeSpan(14, 0, 0);
            }
            else // Weekdays
            {
                return time >= new TimeSpan(10, 30, 0) && time < new TimeSpan(19, 0, 0);
            }
        }
    }



}
