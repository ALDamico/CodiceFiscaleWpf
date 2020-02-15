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
            Validate();
        }

        public bool IsValid { get; private set; }

        public List<ValidationResult> ValidationMessages { get; }

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
           
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                ValidationMessages.Add(new ValidationResult(false,Localization.Localization.ValidationMissingName )); 
            }

            if (string.IsNullOrWhiteSpace(person.Surname))
            {
                ValidationMessages.Add(new ValidationResult(false, Localization.Localization.ValidationMissingSurname));
            }

            if (person.Gender == Gender.Unspecified)
            {
                ValidationMessages.Add(new ValidationResult(false, Localization.Localization.ValidationMissingGender));
            }

            if (person.PlaceOfBirth == null)
            {
                ValidationMessages.Add(new ValidationResult(false, Localization.Localization.ValidationMissingDateOfBirth));
            }

            IsValid = ValidationMessages.Count == 0;
        }
    }
}