using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ALD.LibFiscalCode.StringManipulation
{
    public static class StringExtensions
    {
        public static string ToSentenceCase(this string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    string left = s.Substring(0, i);
                    string right = char.ToUpper(s[i + 1]) + s.Substring(i + 2);
                    s = left + right;
                }
            }
            return s;
        }
    }
}
