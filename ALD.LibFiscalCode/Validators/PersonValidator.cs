using System.Collections.Generic;
using System.Windows;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Localization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class PersonValidator : IValidator
    {
        private readonly Person person;

        public PersonValidator(Person person, LocalizationProvider localizationProvider)
        {
            this.person = person;
            ValidationMessages = new List<string>();
            this.localizationProvider = localizationProvider;
            Validate();
        }

        private readonly LocalizationProvider localizationProvider;

        public bool IsValid { get; private set; }

        public List<string> ValidationMessages { get; }

        public string GetValidationMessagesAsString()
        {
            return string.Concat(ValidationMessages);
        }

        public void Validate()
        {
            var rd = localizationProvider.GetResourceDictionary();
            //TODO Retrieve these messages from data source
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                ValidationMessages.Add(rd["ValidationMissingName"].ToString());
            }

            if (string.IsNullOrWhiteSpace(person.Surname))
            {
                ValidationMessages.Add(rd["ValidationMissingSurname"].ToString());
            }

            if (person.Gender == Gender.Unspecified)
            {
                ValidationMessages.Add(rd["ValidationMissingGender"].ToString());
            }

            if (person.PlaceOfBirth == null)
            {
                ValidationMessages.Add(rd["ValidationMissingDateOfBirth"].ToString());
            }

            IsValid = ValidationMessages.Count == 0;
        }
    }
}