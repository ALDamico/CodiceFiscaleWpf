using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class NameValidationAction:IValidationCommand<Person>
    {
        public void ExecuteValidation(Person person)
        {
            ValidationResult result = null;
            if (person == null)
            {
                Result = new ValidationResult(false, Localization.LocalizationStrings.ValidationArgumentNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                result = new ValidationResult(false,Localization.LocalizationStrings.ValidationMissingName); 
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