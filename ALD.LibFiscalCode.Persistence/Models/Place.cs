using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class Place
    {
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
