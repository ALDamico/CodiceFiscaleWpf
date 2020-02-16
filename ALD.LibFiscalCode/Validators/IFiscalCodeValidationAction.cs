namespace ALD.LibFiscalCode.Validators
{
    public interface IFiscalCodeValidationAction
    {
        bool IsCompleted { get; }
        bool? IsValid { get; }
        void ExecuteValidation();
        ValidationResult Result { get; }
    }
}