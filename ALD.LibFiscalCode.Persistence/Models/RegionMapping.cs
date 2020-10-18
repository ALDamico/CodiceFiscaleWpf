using System.Collections.Generic;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class RegionMapping
    {
        public List<ProvinceMapping> Provinces { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}