namespace MotorbikeRental.Extensions
{
    public static class StringExtension
    {
        public static string RemoveSpecialCharacters(this string value)
            => new(value.Where(char.IsDigit).ToArray());
    }
}