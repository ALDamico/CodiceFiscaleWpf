using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class NameValidationCommand:ValidationCommandBase
    {
        public NameValidationCommand(FiscalCode fiscalCode, Person person):base(fiscalCode, person)
        {
            ExecuteValidation();
        }

        public sealed override void ExecuteValidation()
        {
            
            if (builder.ComputedFiscalCode.Name == fiscalCode.Name)
            {
                IsValid = true;
                Result = CreateValidationResult("La rappresentazione del nome è una stringa valida");
            }
            else
            {
                IsValid = false;
                Result = CreateValidationResult("La rappresentazione del nome non corrisponde a quella della persona");
            }

            IsCompleted = true;
        }
    }
}