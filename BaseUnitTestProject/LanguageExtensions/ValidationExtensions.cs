using System.Text.RegularExpressions;

namespace BaseUnitTestProject.LanguageExtensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Validate string can represent a SSN
        /// </summary>
        /// <param name="sender">value to determine if a valid SSN</param>
        /// <param name="withDash">true if string has dashes</param>
        /// <returns>can represent an SSN</returns>
        /// <remarks>
        /// This is a very simply version, there are other more complex versions
        /// </remarks>
        public static bool IsValidSsn(this string sender, bool withDash = false)
        {
            var pattern = @"^\d{9}$";

            if (withDash)
            {
                pattern = @"^\d{3}-\d{2}-\d{4}$";
            }
            else
            {
                sender = sender.Replace("-", "");
            }

            var regexItem = new Regex(pattern);
            var matcher = regexItem.Match(sender);

            return matcher.Success;

        }
    }
}