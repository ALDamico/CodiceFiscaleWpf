using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.FiscalCode
{
    public class FiscalCodeValidationRunner
    {
        public FiscalCodeValidationRunner(Persistence.Models.FiscalCode target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            
            this.target = target;
            validationCommands = new List<IValidationCommand<Persistence.Models.FiscalCode>>();
        }

        private readonly List<IValidationCommand<Persistence.Models.FiscalCode>> validationCommands;

        private readonly Persistence.Models.FiscalCode target;

        public void AddValidationStep(IValidationCommand<Persistence.Models.FiscalCode> validationCommand)
        {
            validationCommands.Add(validationCommand);
        }

        public List<ValidationResult> Validate()
        {
            var validationResults = new List<ValidationResult>();
            foreach (var command in validationCommands)
            {
                command.ExecuteValidation(target);
                validationResults.Add(command.Result);
            }

            return validationResults;
        }
    }
}