namespace ALD.LibFiscalCode.Persistence.Models
{
    public class FiscalCode
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirthAndGender { get; set; }
        public string PlaceCode { get; set; }
        public string CheckDigit { get; set; }
        public string FullFiscalCode => Surname + Name + DateOfBirthAndGender + PlaceCode + CheckDigit;

        public override string ToString()
        {
            return FullFiscalCode;
        }
    }
}