// ReSharper disable CheckNamespace

using System;
using System.Text;

namespace CodeX.Strings
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Contains code extensions for strings
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Fills the specified format string.
        /// </summary>
        /// <param name="formatString">The format string.</param>
        /// <param name="args">The args.</param>
        /// <returns>Filled string</returns>
        /// <example><code>
        /// "Hello {0} {1}".Fill("Death","Star")
        /// </code><para>returns <i>Hello Death Star</i></para></example>
        public static string Fill(this string formatString, params object[] args)
        {
            return String.Format(formatString, args);
        }


        /// <summary>
        /// Encodes to given string to given to other Encoding format
        /// </summary>
        /// <param name="s">The string to be Encoded.</param>
        /// <param name="encoding">The encoding type</param>
        /// <returns>
        /// Encoded String
        /// </returns>
        public static string Encode(this string s, Encoding encoding)
        {
            return encoding.GetString(Encoding.GetEncoding(encoding.BodyName).GetBytes(s));
        }


        /// <summary>
        /// Converts string to UTF8 format
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>Encoded String</returns>
        public static string ToUTF8(this string s)
        {
            var encoding = Encoding.UTF8;
            return s.Encode(encoding);
        }

        /// <summary>
        /// Converts string to UTF7 format
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>Encoded String</returns>
        public static string ToUTF7(this string s)
        {
            var encoding = Encoding.UTF7;
            return s.Encode(encoding);
        }

        /// <summary>
        /// Converts string to UTF32 format
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>Encoded String</returns>
        public static string ToUTF32(this string s)
        {
            var encoding = Encoding.UTF32;
            return s.Encode(encoding);
        }

        /// <summary>
        /// Converts string to ASCII format
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>Encoded String</returns>
        public static string ToASCII(this string s)
        {
            return Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(s));
        }

        /// <summary>
        /// Converts string to BigEndianUnicode format
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>Encoded String</returns>
        public static string ToBigEndianUnicode(this string s)
        {
            var encoding = Encoding.BigEndianUnicode;
            return s.Encode(encoding);
        }

        /// <summary>
        /// Converts string to Unicode format
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>Encoded String</returns>
        public static string ToUnicode(this string s)
        {
            var encoding = Encoding.Unicode;
            return s.Encode(encoding);
        }


        /// <summary>
        /// Converts string to Base64 format
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="start">The start of the string.</param>
        /// <param name="length">The length of the string.</param>
        /// <param name="options">The Base64FormattingOptions.</param>
        /// <returns>
        /// Base64 String
        /// </returns>
        public static string ToBase64(this string s, int start, int length, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return Convert.ToBase64String(Encoding.GetEncoding(s).GetBytes(s), options);
        }


        /// <summary>
        /// Converts string to Base64 format.
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="options">Base64FormattingOptions.</param>
        /// <returns>
        /// Base64 String
        /// </returns>
        public static string ToBase64(this string s, Base64FormattingOptions options = Base64FormattingOptions.None)
        {
            return s.ToBase64(0, s.Length, options);
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified string].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Determines whether [is null or white space] [the specified string].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is null or white space] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// To the bytes.
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>Bytes using default encoding</returns>
        public static byte[] GetBytes(this string s)
        {
            return Encoding.Default.GetBytes(s);
        }

        /// <summary>
        /// To the bytes.
        /// </summary>
        /// <param name="s">string.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// Bytes using given encoding
        /// </returns>
        public static byte[] GetBytes(this string s,Encoding encoding)
        {
            return encoding.GetBytes(s);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="bytes">input bytes.</param>
        /// <returns>output string</returns>
        public static string GetString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="bytes">input bytes.</param>
        /// <param name="start">start.</param>
        /// <param name="count">count.</param>
        /// <returns>output string</returns>
        public static string GetString(this byte[] bytes,int start,int count)
        {
            return Encoding.Default.GetString(bytes,start,count);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="bytes">input bytes.</param>
        /// <param name="encoding">encoding.</param>
        /// <returns>output string</returns>
        public static string GetString(this byte[] bytes,Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="bytes">input bytes.</param>
        /// <param name="encoding">encoding.</param>
        /// <param name="start">start.</param>
        /// <param name="count">count.</param>
        /// <returns>output string</returns>
        public static string GetString(this byte[] bytes,Encoding encoding, int start, int count)
        {
            return encoding.GetString(bytes, start, count);
        }
     
    }
}