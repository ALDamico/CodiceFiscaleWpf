namespace ALD.LibFiscalCode.Validators.Interfaces
{
    public interface IValidationCommand<TValidationTarget>
    {
        void ExecuteValidation(TValidationTarget validationTarget);
        ValidationResult Result { get; }
    }
}