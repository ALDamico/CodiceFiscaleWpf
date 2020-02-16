using System.Collections.Generic;

namespace ALD.LibFiscalCode.Validators.Interfaces
{
    public interface IValidator
    {
        bool IsValid { get; }

        List<ValidationResult> ValidationMessages { get; }

        void Validate();

        string GetValidationMessagesAsString();
    }
}