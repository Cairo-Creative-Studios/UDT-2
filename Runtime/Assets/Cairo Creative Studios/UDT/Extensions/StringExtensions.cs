using System.Text.RegularExpressions;

namespace Rich.Extensions
{
    public static class StringExtensions 
    {
        public static string AddSpacesBetweenCapitalization(this string input)
        {
            string result = Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
            return result;
        }
    }
}
