﻿using System.Collections.Generic;

namespace ALD.LibFiscalCode.Lookups
{
    public static class CheckDigitLookup
    {
        private static readonly Dictionary<char, int> evenDigits;
        private static readonly Dictionary<char, int> oddDigits;
        private static readonly Dictionary<int, char> outputDigits;

        public static int GetValue(char input, int position)
        {
            if ((position + 1) % 2 == 0)
            {
                return evenDigits[input];
            }
            return oddDigits[input];
        }

        static CheckDigitLookup()
        {
            evenDigits = new Dictionary<char, int>()
            {
                { 'A', 0 },
                { '0', 0 },
                { 'B', 1 },
                { '1', 1 },
                { 'C', 2 },
                { '2', 2 },
                { 'D', 3 },
                { '3', 3 },
                { 'E', 4 },
                { '4', 4 },
                { 'F', 5 },
                { '5', 5 },
                { 'G', 6 },
                { '6', 6 },
                { 'H', 7 },
                { '7', 7 },
                { 'I', 8 },
                { '8', 8 },
                { 'J', 9 },
                { '9', 9 },
                { 'K', 10 },
                { 'L', 11 },
                { 'M', 12 },
                { 'N', 13 },
                { 'O', 14 },
                { 'P', 15 },
                { 'Q', 16 },
                { 'R', 17 },
                { 'S', 18 },
                { 'T', 19 },
                { 'U', 20 },
                { 'V', 21 },
                { 'W', 22 },
                { 'X', 23 },
                { 'Y', 24 },
                { 'Z', 25 }
            };

            oddDigits = new Dictionary<char, int>()
            {
                { 'A', 1 },
                { '0', 1 },
                { 'B', 0 },
                { '1', 0 },
                { 'C', 5 },
                { '2', 5 },
                { 'D', 7 },
                { '3', 7 },
                { 'E', 9 },
                { '4', 9 },
                { 'F', 13 },
                { '5', 13 },
                { 'G', 15 },
                { '6', 15 },
                { 'H', 17 },
                { '7', 17 },
                { 'I', 19 },
                { '8', 19 },
                { 'J', 21 },
                { '9', 21 },
                { 'K', 2 },
                { 'L', 4 },
                { 'M', 18 },
                { 'N', 20 },
                { 'O', 11 },
                { 'P', 3 },
                { 'Q', 6 },
                { 'R', 8 },
                { 'S', 12 },
                { 'T', 14 },
                { 'U', 16 },
                { 'V', 10 },
                { 'W', 22 },
                { 'X', 25 },
                { 'Y', 24 },
                { 'Z', 23 }
            };

            outputDigits = new Dictionary<int, char>()
            {
                {0,  'A'},
                {1,  'B' },
                { 2, 'C' },
                { 3, 'D' },
                { 4, 'E' },
                { 5, 'F' },
                { 6, 'G' },
                { 7, 'H' },
                { 8, 'I' },
                { 9, 'J' },
                { 10, 'K'},
                { 11, 'L' },
                { 12, 'M' },
                { 13, 'N' },
                { 14, 'O' },
                { 15,'P' },
                { 16,'Q' },
                { 17, 'R' },
                { 18,'S' },
                { 19,'T' },
                { 20,'U' },
                { 21, 'V' },
                { 22, 'W' },
                { 23, 'X' },
                { 24, 'Y' },
                { 25, 'Z' }
            };
        }

        public static string GetTranslatedValue(int sum)
        {
            int key = sum % 26;
            return outputDigits[key].ToString();
        }
    }
}
