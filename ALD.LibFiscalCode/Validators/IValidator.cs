namespace ALD.LibFiscalCode.Validators
{
    interface IValidator
    {
        bool IsValid { get; }
        void Validate();
    }
}
