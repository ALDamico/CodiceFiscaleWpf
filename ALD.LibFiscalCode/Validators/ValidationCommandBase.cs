using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public abstract class ValidationCommandBase:IFiscalCodeValidationAction
    {
        public ValidationCommandBase(FiscalCode targetFiscalCode, Person targetPerson)
        {
            person = targetPerson;
            fiscalCode = targetFiscalCode;
            builder = new FiscalCodeBuilder(person);
            IsCompleted = false;
            IsValid = null;
        }
        protected readonly Person person;
        protected readonly FiscalCode fiscalCode;
        protected readonly FiscalCodeBuilder builder;
        public bool IsCompleted { get; protected set; }
        public bool? IsValid { get; protected set; }
        public abstract void ExecuteValidation();

        public ValidationResult Result { get; protected set; }
        protected ValidationResult CreateValidationResult(string text)
        {
            return new ValidationResult(IsValid, text);
        }
    }

   
}