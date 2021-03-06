﻿using System.Globalization;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Localization;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.FiscalCode
{
    public class SurnameValidationCommand : IValidationCommand<Persistence.Models.FiscalCode>
    {
        public SurnameValidationCommand(FiscalCodeBuilder builder)
        {
            this.builder = builder;
        }

        public void ExecuteValidation(Persistence.Models.FiscalCode validationTarget)
        {
            if (validationTarget == null)
            {
                Result = new ValidationResult(false, CodiceFiscaleUI.ValidationArgumentNull);
                return;
            }

            if (validationTarget.Surname == builder.ComputedFiscalCode.Surname)
            {
                Result = new ValidationResult(true, CodiceFiscaleUI.FiscalCodeValidationSurnameValid);
            }
            else
            {
                Result = new ValidationResult(false, CodiceFiscaleUI.FiscalCodeValidationSurnameInvalid,
                    string.Format(CultureInfo.CurrentCulture,
                        CodiceFiscaleUI.FiscalCodeValidationSurnameInvalidDetails,
                        builder.ComputedFiscalCode.Surname));
            }
        }

        private readonly FiscalCodeBuilder builder;
        public ValidationResult Result { get; private set; }
    }
}