using ALD.LibFiscalCode.Persistence.Importer.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ALD.LibFiscalCode.Persistence.Migrations
{
    internal class FormerForeignCountryComparer : IComparer<FormerForeignCountry>
    {
        public int Compare([AllowNull] FormerForeignCountry x, [AllowNull] FormerForeignCountry y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null && y != null)
            {
                return -1;
            }
            if (x != null && y == null)
            {
                return 1;
            }

            if (x.YearOccurred < y.YearOccurred)
            {
                return 1;
            }
            if (x.YearOccurred == y.YearOccurred)
            {
                return 0;
            }
            if (x.YearOccurred > y.YearOccurred)
            {
                return -1;
            }
            return 0; //Unreachable condition, just to make the compiler happy
        }
    }
}