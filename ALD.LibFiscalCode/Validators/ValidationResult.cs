namespace ALD.LibFiscalCode.Validators
{
    public class ValidationResult
    {
        public ValidationResult(bool? isValid)
        {
            IsValid = isValid;
        }
        public ValidationResult(bool? isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public ValidationResult(bool? isValid, string message, string details):this(isValid, message)
        {
            Details = details;
        }
        public bool? IsValid { get; }
        public string Message { get; }
        public string Details { get; set; }
        

        public override string ToString()
        {
            return Message;
        }
    }
}