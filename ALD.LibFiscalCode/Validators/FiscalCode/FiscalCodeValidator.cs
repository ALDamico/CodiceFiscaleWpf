﻿using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Validators.Interfaces;

namespace ALD.LibFiscalCode.Validators.FiscalCode
{
    public class FiscalCodeValidator:IValidator
    {
        public bool IsValid => ValidationMessages?.Count != 0 && ValidationMessages.TrueForAll(m => m.IsValid == true);
        public List<ValidationResult> ValidationMessages { get; private set; }
        private FiscalCodeBuilder builder;
        private Persistence.Models.FiscalCode validationTarget;
        private FiscalCodeValidationRunner runner;

        public FiscalCodeValidator(Persistence.Models.Person person, Persistence.Models.FiscalCode fiscalCode)
        {
            builder = new FiscalCodeBuilder(person);
            validationTarget = fiscalCode;
            runner = new FiscalCodeValidationRunner(fiscalCode);
            Validate();

        }
        public void Validate()
        {
            runner.AddValidationStep(new SurnameValidationCommand(builder));
            runner.AddValidationStep(new NameValidationCommand(builder));
            runner.AddValidationStep(new DateOfBirthAngGenderValidationCommand(builder));
            runner.AddValidationStep(new PlaceCodeValidationCommand(builder));
            runner.AddValidationStep(new CheckDigitValidationCommand(builder));
            ValidationMessages = runner.Validate();
        }

        public string GetValidationMessagesAsString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var message in ValidationMessages)
            {
                stringBuilder.Append(message.Message).Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}