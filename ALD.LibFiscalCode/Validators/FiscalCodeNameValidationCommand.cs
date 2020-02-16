using System.Globalization;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Localization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class FiscalCodeNameValidationCommand : IValidationCommand<FiscalCode>
    {
        public FiscalCodeNameValidationCommand(FiscalCodeBuilder builder)
        {
            this.builder = builder;
        }

        private readonly FiscalCodeBuilder builder;

        public void ExecuteValidation(FiscalCode validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, LocalizationStrings.ValidationArgumentNull);
                return;
            }

            if (validationTarget.Name == builder.ComputedFiscalCode.Name)
            {
                Result = new ValidationResult(true, Localization.LocalizationStrings.FiscalCodeValidationNameValid);
            }
            else
            {
                Result = new ValidationResult(false, Localization.LocalizationStrings.FiscalCodeValidationNameInvalid,
                    string.Format(CultureInfo.CurrentCulture,
                        Localization.LocalizationStrings.FiscalCodeValidationNameInvalidDetails,
                        builder.ComputedFiscalCode.Name));
            }
        }

        public ValidationResult Result { get; private set; }
    }
}