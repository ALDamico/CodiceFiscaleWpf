namespace ALD.LibFiscalCode.Validators
{
    public class ValidationResult
    {
        public ValidationResult(bool? isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
        public bool? IsValid { get; }
        public string Message { get; }

        public override string ToString()
        {
            return Message;
        }
    }
}