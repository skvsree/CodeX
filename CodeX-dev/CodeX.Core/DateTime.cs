// ReSharper disable CheckNamespace
namespace CodeX.Date
// ReSharper restore CheckNamespace
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Contains code extensions for DateTime class
    /// </summary>
    public static class Date
    {
        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="s">Date time string.</param>
        /// <param name="dateTimeFormat">The date time format, default expected format is <c>dd/MM/yyyy</c>.</param>
        /// <returns><c>DateTime</c></returns>
        /// <remarks>
        /// Format the string as per your wish and pass the format as parameter if not passed it will be considered 
        /// as <c>"dd/MM/yyyy"</c> </remarks>
        public static DateTime ToDateTime(this string s, string dateTimeFormat = "dd/MM/yyyy")
        {
            return DateTime.ParseExact(s, dateTimeFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Jan(2000) returns 1st Jan 2000 as DateTime</returns>
        public static DateTime Jan(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Feb(2000) returns 1st Feb 2000 as DateTime</returns>
        public static DateTime Feb(this int day, int year)
        {
            return new DateTime(year, 2, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Mar(2000) returns 1st Mar 2000 as DateTime</returns>
        public static DateTime Mar(this int day, int year)
        {
            return new DateTime(year, 3, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Apr(2000) returns 1st Apr 2000 as DateTime</returns>
        public static DateTime Apr(this int day, int year)
        {
            return new DateTime(year, 4, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.May(2000) returns 1st May 2000 as DateTime</returns>
        public static DateTime May(this int day, int year)
        {
            return new DateTime(year, 5, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Jun(2000) returns 1st Jun 2000 as DateTime</returns>
        public static DateTime Jun(this int day, int year)
        {
            return new DateTime(year, 6, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Jul(2000) returns 1st Jul 2000 as DateTime</returns>
        public static DateTime Jul(this int day, int year)
        {
            return new DateTime(year, 7, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Aug(2000) returns 1st Aug 2000 as DateTime</returns>
        public static DateTime Aug(this int day, int year)
        {
            return new DateTime(year, 8, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Sep(2000) returns 1st Sep 2000 as DateTime</returns>
        public static DateTime Sep(this int day, int year)
        {
            return new DateTime(year, 9, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Oct(2000) returns 1st Oct 2000 as DateTime</returns>
        public static DateTime Oct(this int day, int year)
        {
            return new DateTime(year, 10, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Nov(2000) returns 1st Nov 2000 as DateTime</returns>
        public static DateTime Nov(this int day, int year)
        {
            return new DateTime(year, 11, day);
        }

        /// <summary>
        /// Converts to Date time.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="year">The year.</param>
        /// <returns>1.Dec(2000) returns 1st Dec 2000 as DateTime</returns>
        public static DateTime Dec(this int day, int year)
        {
            return new DateTime(year, 12, day);
        }

        /// <summary>
        /// Determines whether [the specified year] [is leap year].
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        ///   <c>true</c> if [the specified year] [is leap year]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLeapYear(this int year)
        {
            var start = new DateTime(year - 1, 12, 31);
            var end = new DateTime(year, 12, 31);
            return Math.Ceiling(end.Subtract(start).TotalDays) >= 366;
        }
    }
}