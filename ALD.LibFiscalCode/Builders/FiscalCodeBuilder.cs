using ALD.LibFiscalCode.Enums;
using ALD.LibFiscalCode.Lookups;
using ALD.LibFiscalCode.Models;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.StringManipulation;
using System;

namespace ALD.LibFiscalCode.Builders
{
    public class FiscalCodeBuilder
    {
        public FiscalCodeBuilder(Person person)
        {
            if (person == null)
            {
                throw new ArgumentException("Parametro non valido");
            }
            FiscalCode fiscalCode = new FiscalCode();
            fiscalCode.Name = CalculateNameString(person.Name);
            fiscalCode.Surname = CalculateSurnameString(person.Surname);
            fiscalCode.DateOfBirthAndGender = CalculateDateOfBirthAndGenderString(person.DateOfBirth, person.Gender);
            fiscalCode.Place = person.PlaceOfBirth.Code;

            string partial = fiscalCode.Surname + fiscalCode.Name + fiscalCode.DateOfBirthAndGender + fiscalCode.Place;
            fiscalCode.CheckDigit = CalculateCheckDigit(partial);
            ComputedFiscalCode = fiscalCode;
        }

        public FiscalCode ComputedFiscalCode { get; private set; }

        private string CalculateCheckDigit(string input)
        {
            if (input.Length != 15)
            {
                throw new ArgumentException("The input parameter must be a string 15 characters long");
            }
            string output = "";

            int accumulator = 0;

            for (int i = 0; i < 15; i++)
            {
                accumulator += CheckDigitLookup.GetValue(input[i], i);
            }

            output = CheckDigitLookup.GetTranslatedValue(accumulator);

            return output;
        }

        private string CalculateDateOfBirthAndGenderString(DateTime dateOfBirth, Gender gender)
        {
            string output = "";

            string yearOfBirth = dateOfBirth.ToString("yy");
            string monthOfBirth = MonthOfDateLookup.GetValue(dateOfBirth.Month);
            int dayOfBirth = dateOfBirth.Day;
            if (gender == Gender.Female)
            {
                dayOfBirth += 40;
            }

            string dayOfBirthStr = dayOfBirth < 10 ? "0" + dayOfBirth.ToString() : dayOfBirth.ToString();
            output = $"{yearOfBirth}{monthOfBirth}{dayOfBirthStr}";
            return output;
        }

        private string CalculateSurnameString(string input)
        {
            string output = null;

            var nameSplitter = new ConsonantVowelSplitter(input);

            if (nameSplitter.Consonants.Count == 3)
            {
                output = new string(nameSplitter.Consonants.ToArray());
            }
            else if (nameSplitter.Consonants.Count == 2)
            {
                output = new string(new char[3] { nameSplitter.Consonants[0], nameSplitter.Consonants[1], nameSplitter.Vowels[0] });
            }
            else if (nameSplitter.Consonants.Count == 1)
            {
                output = new string(new char[2] { nameSplitter.Consonants[0], nameSplitter.Vowels[0] });
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
                output = new string(new char[3] { nameSplitter.Consonants[0], nameSplitter.Consonants[2], nameSplitter.Consonants[3] });
            }
            else if (nameSplitter.Consonants.Count == 3)
            {
                output = new string(nameSplitter.Consonants.ToArray());
            }
            else if (nameSplitter.Consonants.Count == 2)
            {
                output = new string(new char[3] { nameSplitter.Consonants[0], nameSplitter.Consonants[1], nameSplitter.Vowels[0] });
            }
            else if (nameSplitter.Consonants.Count == 1)
            {
                output = new string(new char[2] { nameSplitter.Consonants[0], nameSplitter.Vowels[0] });
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