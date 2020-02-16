using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class PlaceOfBirthValidationAction:IValidationCommand<Person>
    {
        public void ExecuteValidation(Person validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, Localization.LocalizationStrings.ValidationArgumentNull);
                return;
            }
            if (validationTarget.PlaceOfBirth == null)
            {
               Result = new ValidationResult(false, Localization.LocalizationStrings.ValidationMissingDateOfBirth);
            }
            else
            {
                Result = new ValidationResult(true);
            }
        }

        public ValidationResult Result { get; private set; }
    }
}