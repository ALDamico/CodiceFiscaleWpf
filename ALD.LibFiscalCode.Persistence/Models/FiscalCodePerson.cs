using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class FiscalCodePerson
    {
        public FiscalCodePerson()
        {
        }

        public FiscalCodePerson(Person person, IEnumerable<FiscalCodeDecorator> fiscalCodes)
        {
            if (person == null)
            {
                throw new ArgumentException("The person argument can't be null");
            }

            if (fiscalCodes.Count() != 8)
            {
                throw new ArgumentException("The list of omocodes must contain exactly eight members");
            }

            Person = person;
            FiscalCodes = fiscalCodes as List<FiscalCodeDecorator>;
            FiscalCodes[0].IsMain = true;
        }

        public Person Person { get; set; }

        [NotMapped] public List<FiscalCodeDecorator> FiscalCodes { get; }

        [NotMapped] public FiscalCode MainFiscalCode => FiscalCodes.Where(fc => fc.IsMain).FirstOrDefault()?.FiscalCode;

        [NotMapped] public string MainFiscalCodeString => MainFiscalCode?.FullFiscalCode;

        public void SetMainFiscalCode(FiscalCode fiscalCode)
        {
            var fiscalCodeToUpdate = FiscalCodes.Where(f => f.FiscalCode.Equals(fiscalCode)).FirstOrDefault();
            if (fiscalCodeToUpdate != null)
            {
                FiscalCodes.ForEach(fc => fc.IsMain = false);
                fiscalCodeToUpdate.IsMain = true;
            }
        }

        public void SetMainFiscalCode(int position)
        {
            if (position < 0 || position > 7)
            {
                throw new ArgumentException("The position parameter must be an integer between 0 and 7");
            }

            FiscalCodes.ForEach(fc => fc.IsMain = false);
            FiscalCodes[position].IsMain = true;
        }
    }
}