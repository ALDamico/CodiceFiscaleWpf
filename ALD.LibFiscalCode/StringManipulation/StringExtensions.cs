using System.Globalization;

namespace ALD.LibFiscalCode.StringManipulation
{
    public static class StringExtensions
    {
        public static string ToSentenceCase(this string s)
        {
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    var left = s.Substring(0, i);
                    var right = char.ToUpper(s[i + 1], CultureInfo.InvariantCulture) + s.Substring(i + 2);
                    s = left + right;
                }
            }

            return s;
        }
    }
}