using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.Person
{
    public class SurnameValidationAction:IValidationCommand<Persistence.Models.Person>
    {
        public void ExecuteValidation(Persistence.Models.Person validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, Localization.CodiceFiscaleUI.ValidationArgumentNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(validationTarget.Surname))
            {
                Result = new ValidationResult(false, Localization.CodiceFiscaleUI.ValidationMissingSurname);
            }
            else
            {
                Result = new ValidationResult(true);
            }
        }

        public ValidationResult Result { get; private set; }
    }
}