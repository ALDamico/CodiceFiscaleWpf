using System;
using System.Globalization;
using ALD.LibFiscalCode.Localization;
using ALD.LibFiscalCode.Lookups;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.StringManipulation;

namespace ALD.LibFiscalCode.Builders
{
    public class FiscalCodeBuilder : AbstractNotifyPropertyChanged
    {
        public FiscalCodeBuilder(Person person)
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
        }

        private const int FISCAL_CODE_LENGTH_NO_CHECK_DIGIT = 15;
        private const int FISCAL_CODE_LENGTH_WITH_CHECK_DIGIT = 16;

        public FiscalCodeBuilder(string partial)
        {
            if (partial == null)
            {
                throw new ArgumentNullException(nameof(partial));
            }

            // We're using this to keep track of whether the initial string could possibly be a valid fiscal code 
            // (length between 15 and 16) or not
            var initialLength = partial.Length;

            var fiscalCode = new FiscalCode();

            partial = partial.PadRight(16, ' ');
            fiscalCode.Surname = partial.Substring(0, 3).Trim();
            fiscalCode.Name = partial.Substring(3, 3).Trim();
            fiscalCode.DateOfBirthAndGender = partial.Substring(6, 5).Trim();
            fiscalCode.PlaceCode = partial.Substring(11, 4).Trim();
            fiscalCode.CheckDigit = initialLength switch
            {
                FISCAL_CODE_LENGTH_NO_CHECK_DIGIT => CalculateCheckDigit(partial.Trim()),
                FISCAL_CODE_LENGTH_WITH_CHECK_DIGIT => partial.Substring(15, 1),
                _ => ""
            };
            ComputedFiscalCode = fiscalCode;
        }

        public FiscalCode ComputedFiscalCode { get; }

        internal string CalculateCheckDigit(string input)
        {
            if (input.Length != 15)
            {
                string argumentMessage = CodiceFiscaleUI.BuilderPartialFcLengthException;

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
            var yearOfBirth = dateOfBirth.ToString("yy", CultureInfo.InvariantCulture);
            var monthOfBirth = MonthOfDateLookup.GetValue(dateOfBirth.Month);
            var dayOfBirth = dateOfBirth.Day;
            if (gender == Gender.Female)
            {
                dayOfBirth += 40;
            }

            var dayOfBirthStr = dayOfBirth < 10 ? "0" + dayOfBirth : dayOfBirth.ToString(CultureInfo.InvariantCulture);
            var output = $"{yearOfBirth}{monthOfBirth}{dayOfBirthStr}";
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
                output = new string(nameSplitter.Vowels.GetRange(0, 3).ToArray());
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