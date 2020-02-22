using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.Person
{
    public class PersonValidationRunner
    {
        public PersonValidationRunner(Persistence.Models.Person target)
        {
            person = target ?? throw new ArgumentNullException(nameof(target));
            validationCommands = new List<IValidationCommand<Persistence.Models.Person>>();
        }
        

        private readonly Persistence.Models.Person person;
        public void AddValidationStep(IValidationCommand<Persistence.Models.Person> validationCommand)
        {
            validationCommands.Add(validationCommand);
        }

        private readonly List<IValidationCommand<Persistence.Models.Person>> validationCommands;

        public List<ValidationResult> Validate()
        {
            var validationResults = new List<ValidationResult>();
            foreach (var command in validationCommands)
            {
                command.ExecuteValidation(person);
                validationResults.Add(command.Result);
            }

            return validationResults;
        }
    }
}