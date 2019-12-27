using System.Collections.Generic;
using Unidecode.NET;

namespace ALD.LibFiscalCode.StringManipulation
{
    internal class ConsonantVowelSplitter
    {
        private string word;

        public List<char> Vowels { get; }
        public List<char> Consonants { get; }

        private static readonly List<char> _vowels = new List<char>()
            {
                'A', 'E', 'I', 'O', 'U'
            };
        private static readonly List<char> _consonants = new List<char>()
            {
                'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z'
            };

        public ConsonantVowelSplitter(string word)
        {
            this.word = Unidecoder.Unidecode(word.ToUpperInvariant());
            Vowels = new List<char>();
            Consonants = new List<char>();
            foreach (var letter in this.word)
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
    }
}