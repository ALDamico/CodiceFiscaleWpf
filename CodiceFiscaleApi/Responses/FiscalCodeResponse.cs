using System.Collections.Generic;
using ALD.LibFiscalCode.Validators;

namespace CodiceFiscaleApi.Responses
{
    public class FiscalCodeResponse
    {
        public string Result { get; set; }
        public FiscalCodeJson FiscalCode { get; set; }
        public List<ValidationResult> ValidationResults { get; set; }
    }
}