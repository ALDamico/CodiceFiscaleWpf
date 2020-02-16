using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Validators
{
    public interface IValidationCommand<TValidationTarget>
    {
        void ExecuteValidation(TValidationTarget validationTarget);
        ValidationResult Result { get; }
    }
}