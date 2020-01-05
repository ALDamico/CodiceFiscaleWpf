using System.Collections.Generic;

namespace ALD.LibFiscalCode.Validators
{
    public interface IValidator
    {
        bool IsValid { get; }

        List<string> ValidationMessages { get; }

        void Validate();

        string GetValidationMessagesAsString();
    }
}