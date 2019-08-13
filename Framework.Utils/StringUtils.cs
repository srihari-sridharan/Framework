using System.Globalization;

namespace Framework.Utils
{
    /// <summary>
    ///     Implements extension methods for string.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        ///     Formats the string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="format">The format.</param>
        /// <returns>Formatted string.</returns>
        public static string FormatIt(this string input, params object[] format)
        {
            return string.Format(CultureInfo.CurrentCulture, input, format);
        }

        /// <summary>
        ///     Determines whether [is null or white space] [the specified s].
        /// </summary>
        /// <param name="input">The s.</param>
        /// <returns>True if the string is null, empty or has only white spaces.</returns>
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}