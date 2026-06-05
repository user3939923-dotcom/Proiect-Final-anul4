using System.Text.RegularExpressions;

namespace RestoPro.Helpers
{
    public static class Validator
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsPositiveDecimal(string value,
            out decimal result)
        {
            return decimal.TryParse(value, out result) && result > 0;
        }
    }
}