using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class PersonValidationRunner
    {
        public PersonValidationRunner(Person target)
        {
            person = target ?? throw new ArgumentNullException(nameof(target));
            ValidationCommands = new List<IValidationCommand<Person>>();
        }
        

        private readonly Person person;
        public void AddValidationStep(IValidationCommand<Person> validationCommand)
        {
            ValidationCommands.Add(validationCommand);
        }
        private List<IValidationCommand<Person>> ValidationCommands { get; }

        public List<ValidationResult> Validate()
        {
            var validationResults = new List<ValidationResult>();
            foreach (var command in ValidationCommands)
            {
                command.ExecuteValidation(person);
                validationResults.Add(command.Result);
            }

            return validationResults;
        }
    }
}