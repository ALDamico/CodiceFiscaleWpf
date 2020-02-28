using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.Person
{
    public class NameValidationAction:IValidationCommand<Persistence.Models.Person>
    {
        public void ExecuteValidation(Persistence.Models.Person person)
        {
            ValidationResult result = null;
            if (person == null)
            {
                Result = new ValidationResult(false, Localization.CodiceFiscaleUI.ValidationArgumentNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                result = new ValidationResult(false,Localization.CodiceFiscaleUI.ValidationMissingName); 
            }
            else
            {
                result = new ValidationResult(true);
            }

            Result = result;
        }

        public ValidationResult Result { get; private set; }
    }
}