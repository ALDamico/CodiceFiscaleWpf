using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Lookups
{
    public static class OmocodeLookup
    {
        //https://quifinanza.it/tasse/codice-fiscale-come-si-calcola-e-come-si-corregge-in-caso-di-omocodia/1708/
        private static readonly Dictionary<char, char> values = new Dictionary<char, char>()
        {
            {'0', 'L' },
            {'1', 'M' },
            {'2', 'N' },
            {'3', 'P' },
            {'4', 'Q' },
            {'5', 'R' },
            {'6', 'S' },
            {'7', 'T' },
            {'8', 'U' },
            {'9', 'V' }
        };

        public static char Get(char inputParam)
        {
            try
            {
                return values[inputParam];
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }
    }
}
