namespace ALD.LibFiscalCode.Persistence.Models
{
    public class LocalizedString
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public LanguageInfo Language { get; set; }
        public WindowModel Window { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}