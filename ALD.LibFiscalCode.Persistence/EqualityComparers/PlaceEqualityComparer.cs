using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence.EqualityComparers
{
    public class PlaceEqualityComparer : IEqualityComparer<Place>
    {
        public bool Equals([AllowNull] Place x, [AllowNull] Place y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            if (!x.Name.Equals(y.Name)) return false;
            if (!x.Province.Equals(y.Province)) return false;
            if (!x.StartDate.Equals(y.StartDate)) return false;
            if (!x.EndDate.Equals(y.EndDate)) return false;
            return x.Region.Equals(y.Region);
        }

        public int GetHashCode([DisallowNull] Place obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            return obj.GetHashCode();
        }
    }
}