using System.Collections.Generic;
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
            //TODO Retrieve these messages from data source
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                ValidationMessages.Add("Nome mancante\n");
            }

            if (string.IsNullOrWhiteSpace(person.Surname))
            {
                ValidationMessages.Add("Cognome mancante\n");
            }

            if (person.Gender == Gender.Unspecified)
            {
                ValidationMessages.Add("Sesso non specificato\n");
            }

            if (person.PlaceOfBirth == null)
            {
                ValidationMessages.Add("Luogo di nascita non specificato\n");
            }

            IsValid = ValidationMessages.Count == 0;
        }
    }
}