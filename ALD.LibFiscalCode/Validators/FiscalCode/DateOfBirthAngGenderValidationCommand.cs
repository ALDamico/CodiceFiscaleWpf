using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Localization;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.FiscalCode
{
    public class DateOfBirthAngGenderValidationCommand : IValidationCommand<Persistence.Models.FiscalCode>
    {
        public DateOfBirthAngGenderValidationCommand(FiscalCodeBuilder builder)
        {
            this.builder = builder;
        }

        private readonly FiscalCodeBuilder builder;

        public void ExecuteValidation(Persistence.Models.FiscalCode validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, CodiceFiscaleUI.ValidationArgumentNull);
                return;
            }

            if (validationTarget.DateOfBirthAndGender == builder.ComputedFiscalCode.DateOfBirthAndGender)
            {
                Result = new ValidationResult(true, CodiceFiscaleUI.FiscalCodeValidationBirthDateAndGenderValid);
            }
            else
            {
                Result = new ValidationResult(false, 
                    CodiceFiscaleUI.FiscalCodeValidationBirthDateAndGenderInvalid,
                    CodiceFiscaleUI.FiscalCodeValidationBirthDateAndGenderInvalidDetails);
            }
        }

        public ValidationResult Result { get; private set; }
    }
}