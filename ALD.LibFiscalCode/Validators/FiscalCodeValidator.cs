using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public class FiscalCodeValidator:IValidator
    {
        public bool IsValid => ValidationMessages.Count != 0 && ValidationMessages.TrueForAll(m => m.IsValid == true);
        public List<ValidationResult> ValidationMessages { get; }
        private FiscalCodeBuilder builder;
        private FiscalCode validationTarget;

        public FiscalCodeValidator(Person person, FiscalCode fiscalCode)
        {
            builder = new FiscalCodeBuilder(person);
            validationTarget = fiscalCode;
        }
        public void Validate()
        {
            throw new System.NotImplementedException();
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