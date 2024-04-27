namespace MotorbikeRental.Domain.Extensions
{
    public static class StringExtension
    {
        public static string RemoveSpecialCharacters(this string value)
            => new(value.Where(char.IsDigit).ToArray());

        public static bool ValidateArchiveExtension(this string value, List<string> extensions)
        {
            var extension = Path.GetExtension(value).ToLower();
            return !extensions.Contains(extension);
        }

        public static bool IsBase64String(this string value)
        {
            try
            {
                var data = Convert.FromBase64String(value);
                return Convert.ToBase64String(data) == value;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}