using System.Globalization;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Localization;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.FiscalCode
{
    public class NameValidationCommand : IValidationCommand<Persistence.Models.FiscalCode>
    {
        public NameValidationCommand(FiscalCodeBuilder builder)
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

            if (validationTarget.Name == builder.ComputedFiscalCode.Name)
            {
                Result = new ValidationResult(true, CodiceFiscaleUI.FiscalCodeValidationNameValid);
            }
            else
            {
                Result = new ValidationResult(false, CodiceFiscaleUI.FiscalCodeValidationNameInvalid,
                    string.Format(CultureInfo.CurrentCulture,
                        Localization.CodiceFiscaleUI.FiscalCodeValidationNameInvalidDetails,
                        builder.ComputedFiscalCode.Name));
            }
        }

        public ValidationResult Result { get; private set; }
    }
}