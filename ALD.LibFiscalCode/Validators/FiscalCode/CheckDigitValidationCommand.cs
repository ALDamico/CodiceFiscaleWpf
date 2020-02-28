using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Localization;
using ALD.LibFiscalCode.Validators.Interfaces;


namespace ALD.LibFiscalCode.Validators.FiscalCode
{
    public class CheckDigitValidationCommand : IValidationCommand<Persistence.Models.FiscalCode>
    {
        public CheckDigitValidationCommand(FiscalCodeBuilder builder)
        {
            this.builder = builder;
        }

        private FiscalCodeBuilder builder;

        public void ExecuteValidation(Persistence.Models.FiscalCode validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, CodiceFiscaleUI.ValidationArgumentNull);
                return;
            }

            if (validationTarget.CheckDigit == builder.ComputedFiscalCode.CheckDigit)
            {
                Result = new ValidationResult(true, CodiceFiscaleUI.FiscalCodeValidationCheckDigitValid);
            }
            else
            {
                Result = new ValidationResult(false, CodiceFiscaleUI.FiscalCodeValidationCheckDigitInvalid,
                    CodiceFiscaleUI.FiscalCodeValidationCheckDigitInvalidDetails);
            }
        }

        public ValidationResult Result { get; private set; }
    }
}