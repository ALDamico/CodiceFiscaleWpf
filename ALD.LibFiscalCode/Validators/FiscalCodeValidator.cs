using System.Collections.Generic;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class FiscalCodeValidator:IValidator
    {
        public FiscalCodeValidator(FiscalCode fiscalCode, Person person)
        {
            ValidationMessages = new List<ValidationResult>();
            builder = new FiscalCodeBuilder(person);
        }
        public bool IsValid { get; }
        public List<ValidationResult> ValidationMessages { get; }
        public void Validate()
        {
            throw new System.NotImplementedException();
        }

        public string GetValidationMessagesAsString()
        {
            throw new System.NotImplementedException();
        }

        private FiscalCodeBuilder builder;
        
    }
}