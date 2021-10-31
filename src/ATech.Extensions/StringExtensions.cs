using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace ATech.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if the string is Null or Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Check if the string is Null or White Space
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Check if the string contains only letters (No number or special characters)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetterlOnly(this string str)
        {
            return str.All(c => Char.IsLetter(c) || c == ' ' || c == '\'' || c == '.');
        }

        /// <summary>
        /// Verifies if the string content is a valid email
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            string pattern = @"(?:[a-z0-9!#$%&'*+\/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            return Regex.Match(str, pattern).Success;
        }

        /// <summary>
        /// Checks if the string is a valid Italian Tax Code
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCodiceFiscale(this string str)
        {
            return Regex.Match(str.ToUpper(), @"^(?:[A-Z][AEIOU][AEIOUX]|[B-DF-HJ-NP-TV-Z]{2}[A-Z]){2}(?:[\dLMNP-V]{2}(?:[A-EHLMPR-T](?:[04LQ][1-9MNP-V]|[15MR][\dLMNP-V]|[26NS][0-8LMNP-U])|[DHPS][37PT][0L]|[ACELMRT][37PT][01LM]|[AC-EHLMPR-T][26NS][9V])|(?:[02468LNQSU][048LQU]|[13579MPRTV][26NS])B[26NS][9V])(?:[A-MZ][1-9MNP-V][\dLMNP-V]{2}|[A-M][0L](?:[1-9MNP-V][\dLMNP-V]|[0L][1-9MNP-V]))[A-Z]$").Success;
        }

        /// <summary>
        /// Returns a MemoryStream from a given object
        /// </summary>
        /// <param name="str">The string to process</param>
        /// <param name="encoding">The desired encoding (default is UTF8)</param>
        /// <returns></returns>
        public static Stream ToStream(this string str, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            return new MemoryStream(encoding.GetBytes(str ?? ""));
        }

        /// <summary>
        /// Replaces invalid XML characters in a string with their valid XML equivalent.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Escape(this string str)
        {
            return SecurityElement.Escape(str);
        }

        /// <summary>
        /// Prettyfies the given string 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Capitalize(this string str)
        {
            var tokens = str.Split('\'', StringSplitOptions.RemoveEmptyEntries);

            return tokens.Length switch
            {
                2 => $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tokens[0].ToLower())}\'{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tokens[1].ToLower())}",
                _ => $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower())}"
            };
        }

        /// <summary>
        /// Counts the number of words in a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int CountWords(this string str)
            => str.IsNullOrEmpty() ? 0 : (str.Length - str.Replace(" ", string.Empty).Length + 1);

        /// <summary>
        /// Returns the first word in a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstWord(this string str)
            => str.IsNullOrEmpty() ? str : str.Split(" ", StringSplitOptions.RemoveEmptyEntries).First();

        /// <summary>
        /// Returns the last word in a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LastWord(this string str)
            => str.IsNullOrEmpty() ? str : str.Split(" ", StringSplitOptions.RemoveEmptyEntries).Last();

        /// <summary>
        /// Encodes a plain text in Base64
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string ToBase64(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decodes a Base 64 encoded string to plain text
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string FromBase64(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
