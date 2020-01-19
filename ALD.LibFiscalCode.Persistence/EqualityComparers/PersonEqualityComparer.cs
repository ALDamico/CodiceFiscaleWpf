using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ALD.LibFiscalCode.Persistence.Models;

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

            if (x == null || y == null)
            {
                return false;
            }

            if (!x.Name.Equals(y.Name, StringComparison.InvariantCulture)) return false;
            if (!x.Surname.Equals(y.Surname, StringComparison.InvariantCulture)) return false;
            if (!x.DateOfBirth.Date.Equals(y.DateOfBirth.Date)) return false;
            return x.Gender == y.Gender && x.PlaceOfBirth.Equals(y.PlaceOfBirth);
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
            return obj.GetHashCode();
        }
    }
}