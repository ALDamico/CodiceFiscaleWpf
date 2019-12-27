namespace ALD.LibFiscalCode.Persistence.Models
{
    public class FiscalCode
    {
        public FiscalCode()
        {
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirthAndGender { get; set; }
        public string Place { get; set; }
        public string CheckDigit { get; set; }
        public string Code { get; }
        public string FullFiscalCode => Surname + Name + DateOfBirthAndGender + Place + Code + CheckDigit;
    }
}