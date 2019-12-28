using ALD.LibFiscalCode.Persistence.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ALD.LibFiscalCode.Persistence.EqualityComparers
{
    public class PersonEqualityComparer : IEqualityComparer<Person>
    {
        public bool Equals([AllowNull] Person x, [AllowNull] Person y)
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
                if (x.Surname.Equals(y.Surname))
                {
                    if (x.DateOfBirth.Date.Equals(y.DateOfBirth.Date))
                    {
                        if (x.Gender == y.Gender)
                        {
                            if (x.PlaceOfBirth.Equals(y.PlaceOfBirth))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.GetHashCode();
        }
    }
}