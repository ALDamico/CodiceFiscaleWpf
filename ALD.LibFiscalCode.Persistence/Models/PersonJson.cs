using System;
using ALD.LibFiscalCode.Persistence.Enums;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class PersonJson
    {
        public PersonJson(Person person, FiscalCode fiscalCode)
        {
            this.person = person;
            this.fiscalCode = fiscalCode;
        }
        private readonly Person person;
        private readonly FiscalCode fiscalCode;
        public string Name => person.Name;
        public string Surname => person.Surname;
        public DateTime DateOfBirth => person.DateOfBirth;
        public string Gender => person.Gender.ToString();
        public string PlaceOfBirth => person.PlaceOfBirth.ToString();
        public string FiscalCode => fiscalCode.FullFiscalCode;
    }
}