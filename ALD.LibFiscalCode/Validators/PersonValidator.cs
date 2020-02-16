using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class PersonValidator : IValidator
    {
        private readonly Person person;

        public PersonValidator(Person person)
        {
            this.person = person;
            ValidationMessages = new List<ValidationResult>();
            validationRunner = new PersonValidationRunner(person);
            Validate();
        }

        private readonly PersonValidationRunner validationRunner;

        public bool IsValid => ValidationMessages.Count != 0 && ValidationMessages.TrueForAll(m => m.IsValid == true);

        public List<ValidationResult> ValidationMessages { get; private set; }

        public string GetValidationMessagesAsString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var message in ValidationMessages)
            {
                builder.Append(message.Message).Append("\n");
            }

            return builder.ToString();
        }

        public void Validate()
        {
            validationRunner.AddValidationStep(new NameValidationAction());
            validationRunner.AddValidationStep(new SurnameValidationAction());
            validationRunner.AddValidationStep(new GenderValidationAction());
            validationRunner.AddValidationStep(new PlaceOfBirthValidationAction());

            ValidationMessages = validationRunner.Validate();
        }
    }
}