using System;
using ALD.LibFiscalCode.Persistence.Enums;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class PersonJson
    {
        public PersonJson(Person person)
        {
            this.person = person ?? throw new ArgumentNullException(nameof(person));
            if (person.FiscalCode != null)
            {
                fiscalCodeEntity = person.FiscalCode;
            }
            
        }

        public PersonJson(Person person, FiscalCode fiscalCode)
        {
            this.person = person;
            this.fiscalCode = fiscalCode;
        }

        public PersonJson(Person person, FiscalCodeEntity entity)
        {
            this.person = person;
            this.fiscalCodeEntity = entity;
        }

        private readonly FiscalCodeEntity fiscalCodeEntity;
        private readonly Person person;
        private readonly FiscalCode fiscalCode;
        public string Name => person.Name;
        public string Surname => person.Surname;
        public DateTime DateOfBirth => person.DateOfBirth;
        public string Gender => person.Gender.ToString();
        public string PlaceOfBirth => person.PlaceOfBirth.ToString();
        public string FiscalCode
        {
            get
            {
                if (fiscalCode == null)
                {
                    if (fiscalCodeEntity != null)
                    {
                        return fiscalCodeEntity.FiscalCode;
                    }
                }
                else
                {
                    return fiscalCode.FullFiscalCode;
                }

                return null;
            }
            
        }
        //=> this.fiscalCode != null ? fiscalCode.FullFiscalCode : fiscalCodeEntity.FiscalCode;
    }
}