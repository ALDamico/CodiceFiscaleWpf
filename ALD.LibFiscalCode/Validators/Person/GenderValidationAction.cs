using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.Person
{
    public class GenderValidationAction:IValidationCommand<Persistence.Models.Person>
    {
        public void ExecuteValidation(Persistence.Models.Person validationTarget)
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