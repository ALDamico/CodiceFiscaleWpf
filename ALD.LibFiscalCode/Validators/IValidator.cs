namespace ALD.LibFiscalCode.Validators
{
    internal interface IValidator
    {
        bool IsValid { get; }

        void Validate();
    }
}