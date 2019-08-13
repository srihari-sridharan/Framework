using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Framework.Utils
{
    /// <summary>
    ///     Converts numbers to base 36 strings and vice versa.
    /// </summary>
    public static class Base36Converter
    {
        /// <summary>
        ///     The character set for base 36. 26 alphabets + 10 numerals.
        /// </summary>
        private const string CharacterSet = "0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        ///     The characters set as array.
        /// </summary>
        private static readonly char[] Characters = CharacterSet.ToCharArray();

        /// <summary>
        ///     Decodes the specified base 36 input string as integer.
        /// </summary>
        /// <param name="value">The base 36 input string.</param>
        /// <returns>
        ///     Base 10 number.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int",
            Justification = "Method name clash.")]
        public static int DecodeBase36AsInt(this string value)
        {
            var result = 0;
            var power = 0;
            value = value.ToLower(CultureInfo.CurrentCulture);
            for (var i = value.Length - 1; i >= 0; i--)
            {
                var character = value[i];
                var position = CharacterSet.IndexOf(character);
                if (position > -1)
                    result += position * (int)Math.Pow(CharacterSet.Length, power);
                else
                    return -1;

                power++;
            }

            return result;
        }

        /// <summary>
        ///     Decodes the specified base 36 input string as long.
        /// </summary>
        /// <param name="value">The base 36 input string.</param>
        /// <returns>
        ///     Base 10 number.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "long",
            Justification = "Method name clash.")]
        public static long DecodeBase36AsLong(this string value)
        {
            long result = 0;
            var power = 0;
            value = value.ToLower(CultureInfo.CurrentCulture);
            for (var i = value.Length - 1; i >= 0; i--)
            {
                var character = value[i];
                var position = CharacterSet.IndexOf(character);
                if (position > -1)
                    result += position * (long)Math.Pow(CharacterSet.Length, power);
                else
                    return -1;

                power++;
            }

            return result;
        }

        /// <summary>
        ///     Encodes the specified input number to base 36 string.
        /// </summary>
        /// <param name="inputNumber">The input number.</param>
        /// <returns>
        ///     Base 36 string.
        /// </returns>
        public static string EncodeIntAsBase36(this int inputNumber)
        {
            var base36Builder = new StringBuilder();
            do
            {
                base36Builder.Append(Characters[inputNumber % CharacterSet.Length]);
                inputNumber /= CharacterSet.Length;
            } while (inputNumber != 0);

            var value = base36Builder.ToString();
            return value.Reverse();
        }

        /// <summary>
        ///     Encodes the specified input number to base 36 string.
        /// </summary>
        /// <param name="inputNumber">The input number.</param>
        /// <returns>
        ///     Base 36 string.
        /// </returns>
        public static string EncodeLongAsBase36(this long inputNumber)
        {
            var base36Builder = new StringBuilder();
            do
            {
                base36Builder.Append(Characters[inputNumber % CharacterSet.Length]);
                inputNumber /= CharacterSet.Length;
            } while (inputNumber != 0);

            var value = base36Builder.ToString();
            return value.Reverse();
        }

        /// <summary>
        ///     Reverses the specified string.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Reversed string</returns>
        public static string Reverse(this string input)
        {
            var charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}