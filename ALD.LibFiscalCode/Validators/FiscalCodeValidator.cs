using System.Collections.Generic;

namespace ALD.LibFiscalCode.Validators
{
    public class FiscalCodeValidator:IValidator
    {
        public FiscalCodeValidator()
        {
            ValidationMessages = new List<ValidationResult>();
            
        }
        public bool IsValid { get; }
        public List<ValidationResult> ValidationMessages { get; }
        public void Validate()
        {
            throw new System.NotImplementedException();
        }

        public string GetValidationMessagesAsString()
        {
            throw new System.NotImplementedException();
        }
        
        
    }
}