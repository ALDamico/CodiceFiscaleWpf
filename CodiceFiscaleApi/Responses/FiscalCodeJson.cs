using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Validators.FiscalCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodiceFiscaleApi.Responses
{
    public class FiscalCodeJson
    {
        public FiscalCodeJson(FiscalCode fiscalCode, Person person)
        {
            if (fiscalCode == null)
            {
                throw new ArgumentNullException(nameof(fiscalCode));
            }
            FiscalCode = fiscalCode.FullFiscalCode;
            Name = person.Name;
            Surname = person.Surname;
            Gender = Enum.GetName(typeof(Gender), person.Gender);
            DateOfBirth = person.DateOfBirth;
            PlaceOfBirth = $"{person.PlaceOfBirth.Name} ({person.PlaceOfBirth.ProvinceAbbreviation})";
        }
        public string Name { get; }
        public string Surname { get; }
        public DateTime DateOfBirth { get; }
        public string Gender { get; }
        public string FiscalCode { get; }
        public string PlaceOfBirth { get; }
        
    }
}
