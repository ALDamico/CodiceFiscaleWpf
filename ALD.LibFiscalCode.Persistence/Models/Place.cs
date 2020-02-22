using ALD.LibFiscalCode.Persistence.EqualityComparers;
using System;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class Place
    {
        private readonly PlaceEqualityComparer equalityComparer;

        public Place()
        {
            equalityComparer = new PlaceEqualityComparer();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string ProvinceAbbreviation { get; set; }
        public string Region { get; set; }
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsForeignCountry => ProvinceAbbreviation == "EE";

        public bool Equals(Place other)
        {
            return equalityComparer.Equals(this, other);
        }

        public PlaceEqualityComparer GetEqualityComparer()
        {
            return equalityComparer;
        }

        public override string ToString()
        {
            if (IsForeignCountry)
            {
                return $"{Name} ({Region})";
            }

            return $"{Name} ({ProvinceAbbreviation})";
        }
    }
}