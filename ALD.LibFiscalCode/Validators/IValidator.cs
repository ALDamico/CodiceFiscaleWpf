using System.Collections.Generic;

namespace ALD.LibFiscalCode.Validators
{
    public interface IValidator
    {
        bool IsValid { get; }

        void Validate();

        List<string> ValidationMessages { get; }

        string GetValidationMessagesAsString();
    }
}