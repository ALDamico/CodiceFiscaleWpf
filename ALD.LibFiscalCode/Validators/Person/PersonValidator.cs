using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.Person
{
    public class PersonValidator : IValidator
    {
        private readonly Persistence.Models.Person person;

        public PersonValidator(Persistence.Models.Person person)
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