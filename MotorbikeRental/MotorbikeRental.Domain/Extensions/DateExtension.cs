namespace MotorbikeRental.Domain.Extensions
{
    public static class DateExtension
    {
        public static int DaysDifference(this DateTime dateTime)
        {
            var dateTimeNow = DateTime.Now;
            TimeSpan timeSpan = dateTime.AddDays(1) - dateTimeNow;
            return timeSpan.Days;
        }
    }
}