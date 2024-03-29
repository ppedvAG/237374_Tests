﻿namespace TddBank
{
    public class OpeningHours
    {
        public bool IsWeekend()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Saturday ||
                   DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
        }


        // Example opening hours: 10:30 to 19:00 on weekdays, 10:30 to 14:00 on Saturdays, closed on Sundays
        public bool IsOpen(DateTime dateTime)
        {
            DayOfWeek day = dateTime.DayOfWeek;
            TimeSpan time = dateTime.TimeOfDay;


            if (dateTime.Day == 24 && dateTime.Month == 12)
                return false;

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
