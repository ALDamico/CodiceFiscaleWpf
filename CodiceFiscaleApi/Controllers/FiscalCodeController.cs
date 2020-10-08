using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Builders;
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

        /*// POST: api/FiscalCode
        [HttpPost("calculate")]
        //TODO Creare classe Response per questo endpoint
        public string Post([FromForm] string person, [FromForm] int? placeOfBirthId)
        {
            Log.Information("Requested fiscal code calculation from {0}", HttpContext.Connection.RemoteIpAddress);
            Log.Information("Request details follow");
            Log.Information("Person: {0}", person);
            Log.Information("Place of Birth id: {0}",
                placeOfBirthId == null ? "null" : placeOfBirthId.GetValueOrDefault().ToString());
            var deserializedPerson = JsonConvert.DeserializeObject<Person>(person);
            try
            {
                var validator = new PersonValidator(deserializedPerson);
                Log.Information("Validator response {0} with the following messages", validator.IsValid,
                    validator.GetValidationMessagesAsString());
                if (validator.IsValid)
                {
                    var fc = new FiscalCodeBuilder(deserializedPerson);
                    var outputObj = new
                        {result = "success", fiscalCodeInfo = new FiscalCodeJson(fc.ComputedFiscalCode, deserializedPerson)};
                    var serializedObject =
                        JsonConvert.SerializeObject(outputObj, jsonNetConfiguration.SerializerSettings);
                    return serializedObject;
                }

                var obj = new {result = "failed", payload = validator};
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred when processing the request");
                Log.Error(ex.ToString());
                return JsonConvert.SerializeObject(new {result = "failed", payload = ex.ToString()});
            }
        }*/
        [HttpPost("calculate")]
        public async Task<FiscalCodeResponse> Calculate([FromForm] string request)
        {
            try
            {
                var dateConverter = new IsoDateTimeConverter();
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
        public string Post([FromForm] Person person, [FromForm] int placeOfBirthId, [FromForm] string fiscalCode)
        {
            if (person != null)
            {
                person.PlaceOfBirth = dataContext.Places.SingleOrDefault(p => p.Id == placeOfBirthId);
            }

            var validator = new PersonValidator(person);
            if (validator.IsValid)
            {
                var fc = new FiscalCodeBuilder(fiscalCode);
                var fcValidator = new FiscalCodeValidator(person, fc.ComputedFiscalCode);
                var serializedObject = JsonConvert.SerializeObject(fcValidator, Formatting.Indented,
                    new JsonSerializerSettings() {StringEscapeHandling = StringEscapeHandling.EscapeHtml});
                return serializedObject;
            }

            var obj = new {result = "failed", payload = validator};
            return JsonConvert.SerializeObject(obj);
        }
    }
}
