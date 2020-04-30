using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using ALD.LibFiscalCode.Validators.FiscalCode;
using ALD.LibFiscalCode.Validators.Person;
using CodiceFiscaleApi.Configuration;
using CodiceFiscaleApi.Converters;
using CodiceFiscaleApi.Requests;
using CodiceFiscaleApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;

namespace CodiceFiscaleApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FiscalCodeController : ControllerBase
    {
        private readonly AppDataContext dataContext;
        private readonly JsonNetConfiguration jsonNetConfiguration;

        public FiscalCodeController(AppDataContext dataContext)
        {
            this.dataContext = dataContext;
            jsonNetConfiguration = new JsonNetConfiguration();
        }
        
        [HttpPost("calculate")]
        public async Task<FiscalCodeResponse> Calculate([FromForm] string request)
        {
            try
            {
                var jsonSettings = new JsonSerializerSettings()
                {
                    DateFormatString = "yyyy-MM-dd"
                };
                var deserializedRequest = JsonConvert.DeserializeObject<PersonRequest>(request, jsonSettings);
                Log.Information("Requested fiscal code calculation from {0}", HttpContext.Connection.RemoteIpAddress);
                Log.Information("Request details follow");
                Log.Information("Person: {0}", request);
                FiscalCodeResponse response = new FiscalCodeResponse();
                RequestToPersonConverter converter = new RequestToPersonConverter();
                var person = await converter.ConvertToPersonAsync(dataContext, deserializedRequest)
                    .ConfigureAwait(false);
                Log.Information("Person deserialized");
                var validator = new PersonValidator(person);
                if (validator.IsValid)
                {
                    Log.Information("Computed with success");
                    response.Result = "success";
                    var fc = new FiscalCodeBuilder(person);
                    var fcJson = new FiscalCodeJson(fc.ComputedFiscalCode, person);
                    response.FiscalCode = fcJson;
                }
                else
                {
                    Log.Information("Computed with error");
                    response.Result = "failure";
                    response.FiscalCode = null;
                }

                response.ValidationResults = validator.ValidationMessages;

                Log.Information("Returning response");
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }
            
        }

        [HttpPost("validate")]
        public ValidationResponse ValidateFiscalCode([FromBody] ValidationRequest request)
        { 
            ValidationResponse output = new ValidationResponse();
            if (request == null)
            {
                output.Outcome = false;
                return output;
            }
            Person person = new Person()
            {
                Name = request.Name,
                Surname = request.Surname,
                DateOfBirth = request.BirthDate,
                PlaceOfBirth = dataContext.Places.Single(p => p.Id == request.BirthPlaceId),
                Gender = (Gender) Enum.Parse(typeof(Gender), request.Gender)
            };

            var personValidator = new PersonValidator(person);
            if (personValidator.IsValid)
            {
                var fiscalCodeBuilder = new FiscalCodeBuilder(request.FiscalCode.ToUpperInvariant());
                var fiscalCodeValidator = new FiscalCodeValidator(person, fiscalCodeBuilder.ComputedFiscalCode);
                output.ExpectedFiscalCode = fiscalCodeValidator.ExpectedFiscalCode;
                output.ProvidedFiscalCode = fiscalCodeValidator.ProvidedFiscalCode;
                output.Person = new PersonJson(person);
                output.Outcome = false;
                output.ValidationMessages = fiscalCodeValidator.ValidationMessages;
                if (fiscalCodeValidator.IsValid)
                {
                    output.Outcome = true;
                }
            }
            
            return output;
        }
    }
}
