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
                Result = new ValidationResult(false, LocalizationStrings.ValidationArgumentNull);
                return;
            }

            if (validationTarget.Name == builder.ComputedFiscalCode.Name)
            {
                Result = new ValidationResult(true, LocalizationStrings.FiscalCodeValidationNameValid);
            }
            else
            {
                Result = new ValidationResult(false, LocalizationStrings.FiscalCodeValidationNameInvalid,
                    string.Format(CultureInfo.CurrentCulture,
                        Localization.LocalizationStrings.FiscalCodeValidationNameInvalidDetails,
                        builder.ComputedFiscalCode.Name));
            }
        }

        public ValidationResult Result { get; private set; }
    }
}