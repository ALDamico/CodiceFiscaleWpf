using ALD.LibFiscalCode.Persistence.Models;
using System.Collections.Generic;

namespace ALD.LibFiscalCode.Validators
{
    public class PersonValidator : IValidator
    {
        public PersonValidator(Person person)
        {
            this.person = person;
            ValidationMessages = new List<string>();
            Validate();
        }

        private Person person;

        public bool IsValid
        {
            get;
            private set;
        }

        public List<string> ValidationMessages { get; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                ValidationMessages.Add("Nome mancante\n");
            }

            if (string.IsNullOrWhiteSpace(person.Surname))
            {
                ValidationMessages.Add("Cognome mancante\n");
            }

            if (person.Gender == Enums.Gender.Unspecified)
            {
                ValidationMessages.Add("Sesso non specificato\n");
            }

            if (person.PlaceOfBirth == null)
            {
                ValidationMessages.Add("Luogo di nascita non specificato\n");
            }

            if (ValidationMessages.Count == 0)
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
        }
    }
}