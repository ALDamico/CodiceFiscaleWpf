using ALD.LibFiscalCode.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

            if (x == null && y != null || x != null && y == null)
            {
                return false;
            }

            if (x.Name.Equals(y.Name))
            {
                if (x.Province.Equals(y.Province))
                {
                    if (x.Region.Equals(y.Region))
                    {
                        return true;
                    }
                }
            }

            return false;
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Place obj)
        {
            return obj.GetHashCode();
        }
    }
}