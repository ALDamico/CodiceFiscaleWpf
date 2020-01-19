using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Localization;

namespace ALD.LibFiscalCode.Lookups
{
    public static class MonthOfDateLookup
    {
        private static readonly Dictionary<int, string> Lookup = new Dictionary<int, string>
        {
            {1, "A"},
            {2, "B"},
            {3, "C"},
            {4, "D"},
            {5, "E"},
            {6, "H"},
            {7, "L"},
            {8, "M"},
            {9, "P"},
            {10, "R"},
            {11, "S"},
            {12, "T"}
        };

     
        public static string GetValue(int input)
        {
            if (input < 1 || input > 12)
            {
                throw new ArgumentException(Localization.Localization.MonthOfDateRangeError);
            }

            return Lookup[input];
        }
    }
}