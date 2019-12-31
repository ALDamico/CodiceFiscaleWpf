using ALD.LibFiscalCode.Persistence.EqualityComparers;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class Place
    {
        public int Id { get; set; }
        public Place()
        {
            equalityComparer = new PlaceEqualityComparer();
        }

        public bool Equals(Place other)
        {
            return equalityComparer.Equals(this, other);
        }

        public PlaceEqualityComparer GetEqualityComparer()
        {
            return equalityComparer;
        }
        private PlaceEqualityComparer equalityComparer;
        public string Name { get; set; }
        public string Province { get; set; }
        public string ProvinceAbbreviation { get; set; }
        public string Region { get; set; }
        public string Code { get; set; }

        public bool IsForeignCountry => ProvinceAbbreviation == "EE";

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