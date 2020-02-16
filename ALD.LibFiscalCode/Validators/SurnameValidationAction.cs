using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class SurnameValidationAction:IValidationCommand<Person>
    {
        public void ExecuteValidation(Person validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, Localization.LocalizationStrings.ValidationArgumentNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(validationTarget.Surname))
            {
                Result = new ValidationResult(false, Localization.LocalizationStrings.ValidationMissingSurname);
            }
            else
            {
                Result = new ValidationResult(true);
            }
        }

        public ValidationResult Result { get; private set; }
    }
}