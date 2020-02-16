using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class BirthDateGenderValidationCommand:ValidationCommandBase
    {
        public BirthDateGenderValidationCommand(FiscalCode fiscalCode, Person person):base(fiscalCode, person)
        {
            ExecuteValidation();
        }

        public sealed override  void ExecuteValidation()
        {
            if (builder.ComputedFiscalCode.DateOfBirthAndGender == fiscalCode.DateOfBirthAndGender)
            {
                IsValid = true;
                CreateValidationResult("La data di nascita e il sesso corrispondono");
            }
            else
            {
                IsValid = false;
            }

            IsCompleted = true;
        }
    }
}