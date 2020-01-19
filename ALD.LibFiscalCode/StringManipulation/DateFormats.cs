using System.Globalization;

namespace ALD.LibFiscalCode.StringManipulation
{
    public enum DateFormat
    {
        /// <summary>
        /// Short Date ("d") Format Specifier: represents a custom date and time format string that is defined by a specific culture's <see cref="DateTimeFormatInfo.ShortDatePattern"/> property.
        /// </summary>
        ShortDate = 'd',
        /// <summary>
        /// Long Date ("D") Format Specifier: represents a custom date and time format string that is defined by the current <see cref="DateTimeFormatInfo.LongDatePattern"/> property.
        /// </summary>
        ExtendedDate = 'D',
        /// <summary>
        /// Long Date ("D") Format Specifier: represents a combination of the long date ("D") and short time ("t") patterns, separated by a space.
        /// </summary>
        FullDateShortTime = 'f',
        /// <summary>
        /// Full Date Long Time ("F") Format Specifier: represents a custom date and time format string that is defined by the current <see cref="DateTimeFormatInfo.FullDateTimePattern"/> property.
        /// </summary>
        FullDateExtendedTime = 'F',
        /// <summary>
        /// General Date Short Time ("g") Format Specifier: represents a combination of the short date ("d") and short time ("t") patterns, separated by a space.
        /// </summary>
        GeneralDateTimeShortTime = 'g',
        /// <summary>
        /// General Date Long Time ("G") Format Specifier: represents a combination of the short date ("d") and long time ("T") patterns, separated by a space.
        /// </summary>
        GeneralDateTimeExtendedTime = 'G',
        /// <summary>
        /// Month ("M", "m") Format Specifier: represents a custom date and time format string that is defined by the current DateTimeFormatInfo.MonthDayPattern property.
        /// </summary>
        MonthDaySchema = 'm',
        /// <summary>
        /// Round-trip ("O", "o") Format Specifier: represents a custom date and time format string using a pattern that preserves time zone information and emits a result string that complies with ISO 8601.
        /// </summary>
        RoundTripSchema = 'o',
        /// <summary>
        /// RFC1123 ("R", "r") Format Specifier: represents a custom date and time format string that is defined by the <see cref="DateTimeFormatInfo.RFC1123Pattern"/> property.
        /// The pattern reflects a defined standard, and the property is read-only.
        /// </summary>
        Rfc1123Schema = 'r',
        /// <summary>
        /// Sortable ("s") Format Specifier: represents a custom date and time format string that is defined by the <see cref="DateTimeFormatInfo.SortableDateTimePattern"/> property.
        /// The pattern reflects a defined standard (ISO 8601), and the property is read-only.
        /// </summary>
        Sortable = 's',
        /// <summary>
        /// Short Time ("t") Format Specifier: represents a custom date and time format string that is defined by the current <see cref="DateTimeFormatInfo.ShortTimePattern"/> property.
        /// </summary>
        ShortTime = 't',
        /// <summary>
        /// Long Time ("T") Format Specifier: represents a custom date and time format string that is defined by a specific culture's <see cref="DateTimeFormatInfo.LongTimePattern"/> property.
        /// </summary>
        ExtendedTime = 'T',
        /// <summary>
        /// Universal Sortable ("u") Format Specifier: specifier represents a custom date and time format string that is defined by the <see cref="DateTimeFormatInfo.UniversalSortableDateTimePattern "/> property.
        /// </summary>
        UniversalSortable = 'u',
        /// <summary>
        /// Universal Full ("U") Format Specifier: represents a custom date and time format string that is defined by a specified culture's <see cref="DateTimeFormatInfo.FullDateTimePattern"/> property.
        /// The pattern is the same as the "F" pattern.
        /// However, the DateTime value is automatically converted to UTC before it is formatted.
        /// </summary>
        UniversalSortableComplete = 'U',
        /// <summary>
        /// Year Month ("Y", "y") Format Specifier: represents a custom date and time format string that is defined by the <see cref="DateTimeFormatInfo.YearMonthPattern"/> property of a specified culture.
        /// </summary>
        YearMonthSchema = 'y',
        FilenameSortable
    }
}