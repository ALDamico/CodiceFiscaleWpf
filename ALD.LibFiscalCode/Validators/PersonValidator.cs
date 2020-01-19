using System.Collections.Generic;
using System.Windows;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Localization;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Localization;

namespace ALD.LibFiscalCode.Validators
{
    public class PersonValidator : IValidator
    {
        private readonly Person person;

        public PersonValidator(Person person)
        {
            this.person = person;
            ValidationMessages = new List<string>();
            Validate();
        }

        public bool IsValid { get; private set; }

        public List<string> ValidationMessages { get; }

        public string GetValidationMessagesAsString()
        {
            return string.Concat(ValidationMessages);
        }

        public void Validate()
        {
           
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                ValidationMessages.Add(Localization.Localization.ValidationMissingName);
            }

            if (string.IsNullOrWhiteSpace(person.Surname))
            {
                ValidationMessages.Add(Localization.Localization.ValidationMissingSurname);
            }

            if (person.Gender == Gender.Unspecified)
            {
                ValidationMessages.Add(Localization.Localization.ValidationMissingGender);
            }

            if (person.PlaceOfBirth == null)
            {
                ValidationMessages.Add(Localization.Localization.ValidationMissingDateOfBirth);
            }

            IsValid = ValidationMessages.Count == 0;
        }
    }
}