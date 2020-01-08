using System;
using System.Globalization;
using ALD.LibFiscalCode.Lookups;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Localization;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.StringManipulation;

namespace ALD.LibFiscalCode.Builders
{
    public class FiscalCodeBuilder : AbstractNotifyPropertyChanged
    {
        public FiscalCodeBuilder(Person person, LocalizationProvider localizationProvider)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var fiscalCode = new FiscalCode();
            fiscalCode.Name = CalculateNameString(person.Name);
            fiscalCode.Surname = CalculateSurnameString(person.Surname);
            fiscalCode.DateOfBirthAndGender = CalculateDateOfBirthAndGenderString(person.DateOfBirth, person.Gender);
            fiscalCode.PlaceCode = person.PlaceOfBirth.Code;

            var partial = fiscalCode.Surname + fiscalCode.Name + fiscalCode.DateOfBirthAndGender + fiscalCode.PlaceCode;
            fiscalCode.CheckDigit = CalculateCheckDigit(partial);
            ComputedFiscalCode = fiscalCode;
            this.localizationProvider = localizationProvider;
        }

        private readonly LocalizationProvider localizationProvider;

        public FiscalCodeBuilder(string partial)
        {
            if (partial == null)
            {
                throw new ArgumentNullException(nameof(partial));
            }

            if (partial.Length != 15)
            {
                string argumentMessage = localizationProvider != null ? localizationProvider.GetResourceDictionary()["BuilderPartialFcLengthException"].ToString() : "";
                throw new ArgumentException(argumentMessage);
            }

            var fiscalCode = new FiscalCode();
            fiscalCode.Surname = partial.Substring(0, 3);
            fiscalCode.Name = partial.Substring(3, 3);
            fiscalCode.DateOfBirthAndGender = partial.Substring(6, 5);
            fiscalCode.PlaceCode = partial.Substring(10, 4);
            fiscalCode.CheckDigit = CalculateCheckDigit(partial);
            ComputedFiscalCode = fiscalCode;
        }

        public FiscalCode ComputedFiscalCode { get; }

        internal string CalculateCheckDigit(string input)
        {
            if (input.Length != 15)
            {
                string argumentMessage = null;

                if (localizationProvider != null)
                {
                    argumentMessage = localizationProvider.GetResourceDictionary()["BuilderPartialFcLengthException"].ToString();
                }
                else
                {
                    argumentMessage = "";
                }
                throw new ArgumentException(argumentMessage);
            }

            var accumulator = 0;

            for (var i = 0; i < 15; i++)
            {
                accumulator += CheckDigitLookup.GetValue(input[i], i);
            }

            var output = CheckDigitLookup.GetTranslatedValue(accumulator);
            return output;
        }

        private string CalculateDateOfBirthAndGenderString(DateTime dateOfBirth, Gender gender)
        {
            var output = "";

            var yearOfBirth = dateOfBirth.ToString("yy", CultureInfo.InvariantCulture);
            var monthOfBirth = MonthOfDateLookup.GetValue(dateOfBirth.Month);
            var dayOfBirth = dateOfBirth.Day;
            if (gender == Gender.Female)
            {
                dayOfBirth += 40;
            }

            var dayOfBirthStr = dayOfBirth < 10 ? "0" + dayOfBirth : dayOfBirth.ToString(CultureInfo.InvariantCulture);
            output = $"{yearOfBirth}{monthOfBirth}{dayOfBirthStr}";
            return output;
        }

        private string CalculateSurnameString(string input)
        {
            string output = null;

            var nameSplitter = new ConsonantVowelSplitter(input);

            if (nameSplitter.Consonants.Count >= 3)
            {
                output = new string(nameSplitter.Consonants.ToArray().AsSpan(0, 3).ToArray());
            }
            else if (nameSplitter.Consonants.Count == 2)
            {
                output = new string(new[]
                    {nameSplitter.Consonants[0], nameSplitter.Consonants[1], nameSplitter.Vowels[0]});
            }
            else if (nameSplitter.Consonants.Count == 1)
            {
                output = new string(new[] { nameSplitter.Consonants[0], nameSplitter.Vowels[0] });
                if (nameSplitter.Vowels.Count >= 2)
                {
                    output += nameSplitter.Vowels[1];
                }
            }
            else
            {
                output = new string(nameSplitter.Vowels.ToArray());
            }

            if (output.Length == 2)
            {
                output += "X";
            }

            return output;
        }

        private string CalculateNameString(string input)
        {
            string output = null;

            var nameSplitter = new ConsonantVowelSplitter(input);

            if (nameSplitter.Consonants.Count >= 4)
            {
                output = new string(new[]
                    {nameSplitter.Consonants[0], nameSplitter.Consonants[2], nameSplitter.Consonants[3]});
            }
            else if (nameSplitter.Consonants.Count == 3)
            {
                output = new string(nameSplitter.Consonants.ToArray().AsSpan(0, 3).ToArray());
            }
            else if (nameSplitter.Consonants.Count == 2)
            {
                output = new string(new[]
                    {nameSplitter.Consonants[0], nameSplitter.Consonants[1], nameSplitter.Vowels[0]});
            }
            else if (nameSplitter.Consonants.Count == 1)
            {
                output = new string(new[] { nameSplitter.Consonants[0], nameSplitter.Vowels[0] });
                if (nameSplitter.Vowels.Count >= 2)
                {
                    output += nameSplitter.Vowels[1];
                }
            }
            else
            {
                output = new string(nameSplitter.Vowels.ToArray());
            }

            if (output.Length == 2)
            {
                output += "X";
            }

            return output;
        }
    }
}