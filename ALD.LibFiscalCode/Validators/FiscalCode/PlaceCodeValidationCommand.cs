using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Localization;
using ALD.LibFiscalCode.Validators.Interfaces;
using System.Globalization;

namespace ALD.LibFiscalCode.Validators.FiscalCode
{
    public class PlaceCodeValidationCommand : IValidationCommand<Persistence.Models.FiscalCode>
    {
        public PlaceCodeValidationCommand(FiscalCodeBuilder builder)
        {
            this.builder = builder;
        }

        private FiscalCodeBuilder builder;

        public void ExecuteValidation(Persistence.Models.FiscalCode validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, LocalizationStrings.ValidationArgumentNull);
                return;
            }

            if (validationTarget.PlaceCode == builder.ComputedFiscalCode.PlaceCode)
            {
                Result = new ValidationResult(true, LocalizationStrings.FiscalCodeValidationPlaceCodeValid);
            }
            else
            {
                Result = new ValidationResult(false, 
                    LocalizationStrings.FiscalCodeValidationPlaceCodeInvalid,
                   string.Format(CultureInfo.CurrentCulture, LocalizationStrings.FiscalCodeValidationPlaceCodeInvalidDetails, builder.ComputedFiscalCode.PlaceCode));
            }
        }

        public ValidationResult Result { get; private set; }
    }
}