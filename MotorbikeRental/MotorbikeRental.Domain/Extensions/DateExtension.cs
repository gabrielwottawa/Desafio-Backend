namespace MotorbikeRental.Domain.Extensions
{
    public static class DateExtension
    {
        public static int DaysDifference(this DateTime dateTime1, DateTime dateTime2, int extraDays = 0)
        {
            TimeSpan timeSpan = dateTime1.AddDays(extraDays) - dateTime2;
            return timeSpan.Days;
        }
    }
}