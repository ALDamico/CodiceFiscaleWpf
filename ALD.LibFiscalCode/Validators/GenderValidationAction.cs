using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class GenderValidationAction:IValidationCommand<Person>
    {
        public void ExecuteValidation(Person validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, Localization.LocalizationStrings.ValidationArgumentNull);
                return;
            }
            if (validationTarget.Gender == Gender.Unspecified)
            {
                Result = new ValidationResult(false, Localization.LocalizationStrings.ValidationMissingGender);
            }
            else
            {
                Result = new ValidationResult(true);
            }
        }

        public ValidationResult Result { get; private set; }
    }
}