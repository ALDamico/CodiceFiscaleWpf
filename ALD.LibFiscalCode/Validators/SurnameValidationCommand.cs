using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class SurnameValidationCommand:ValidationCommandBase
    {
        public SurnameValidationCommand(FiscalCode fiscalCode, Person person):base(fiscalCode, person)
        {
            ExecuteValidation();
        }
        
        public sealed override void ExecuteValidation()
        {
            //TODO Move these string literals into Localization namespace
            if (builder.ComputedFiscalCode.Surname == fiscalCode.Surname)
            {
                IsValid = true;
                CreateValidationResult("La rappresentazione del cognome è valida.");
            }
            else
            {
                IsValid = false;
                CreateValidationResult("La rappresentazione del cognome non è valida.");
            }

            IsCompleted = true;
        }

        
        
    }
}