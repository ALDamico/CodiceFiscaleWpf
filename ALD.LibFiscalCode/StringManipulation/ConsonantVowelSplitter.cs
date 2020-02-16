using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
using ALD.LibFiscalCode.Settings;
using Unidecode.NET;

namespace ALD.LibFiscalCode.StringManipulation
{
    internal class ConsonantVowelSplitter
    {
        private static readonly List<char> _vowels = new List<char>
        {
            'A', 'E', 'I', 'O', 'U'
        };

        private static readonly List<char> _consonants = new List<char>
        {
            'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z'
        };

        public ConsonantVowelSplitter(string word)
        {
            using var database = new AppDataContext();
            var split = AppSettings.GetInstance(database).GetSplittingStrategy(word).Result;
            if (split.Equals(word, StringComparison.InvariantCultureIgnoreCase))
            {
                //Uses unidecode method as a fallback if the previous attempt failed.
                split = new UnidecodeSplittingStrategy(word).Result;
            }
            ExecuteSplit(split.ToUpperInvariant());
        }

        public ConsonantVowelSplitter(string word, ISplittingStrategy strategy)
        {
            word = strategy.Result;
            ExecuteSplit(word.ToUpperInvariant());
        }

        private void ExecuteSplit(string word)
        {
            Vowels = new List<char>();
            Consonants = new List<char>();
            foreach (var letter in word)
            {
                if (_vowels.Contains(letter))
                {
                    Vowels.Add(letter);
                }
                else if (_consonants.Contains(letter))
                {
                    Consonants.Add(letter);
                }
            }
        }

        public List<char> Vowels { get; private set; }
        public List<char> Consonants { get; private set; }
    }
}