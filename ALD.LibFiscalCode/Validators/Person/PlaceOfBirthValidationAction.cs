using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.Person
{
    public class PlaceOfBirthValidationAction:IValidationCommand<Persistence.Models.Person>
    {
        public void ExecuteValidation(Persistence.Models.Person validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, Localization.CodiceFiscaleUI.ValidationArgumentNull);
                return;
            }
            if (validationTarget.PlaceOfBirth == null)
            {
               Result = new ValidationResult(false, Localization.CodiceFiscaleUI.ValidationMissingDateOfBirth);
            }
            else
            {
                Result = new ValidationResult(true);
            }
        }

        public ValidationResult Result { get; private set; }
    }
}