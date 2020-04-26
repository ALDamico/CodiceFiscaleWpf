using System.Collections.Generic;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Validators;
using Microsoft.AspNetCore.Components.Forms;

namespace CodiceFiscaleApi.Responses
{
    public class ValidationResponse
    {
        public bool Outcome { get; set; }
        public string ExpectedFiscalCode { get; set; }
        public string ProvidedFiscalCode { get; set; }
        public PersonJson Person { get; set; }
        public List<ValidationResult> ValidationMessages { get; set; }
    }
}