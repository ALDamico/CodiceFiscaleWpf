namespace ALD.LibFiscalCode.Persistence.Models
{
    public class ProvinceMapping
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public RegionMapping Region { get; set; }
    }
}