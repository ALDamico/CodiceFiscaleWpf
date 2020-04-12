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
using CodiceFiscaleApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        // POST: api/FiscalCode
        [HttpPost("calculate")]
        public string Post([FromForm] string person, [FromForm] int? placeOfBirthId)
        {
            try
            {
                Log.Information("Requested fiscal code calculation from {0}", HttpContext.Connection.RemoteIpAddress);
                Log.Information("Request details follow");
                Log.Information("Person: {0}", person);
                Log.Information("Place of Birth id: {0}",
                    placeOfBirthId == null ? "null" : placeOfBirthId.GetValueOrDefault().ToString());
                Person deserialized = new Person();
                try
                {
                    deserialized =
                        JsonConvert.DeserializeObject(person, typeof(Person),
                                jsonNetConfiguration.SerializerSettings) as
                            Person;
                    Log.Debug("Deserialized object: {0}", deserialized);
                }
                catch (JsonException ex)
                {
                    Log.Error("An error occurred");
                    Log.Error(ex.ToString());
                }

                var validator = new PersonValidator(deserialized);
                Log.Information("Validator response {0} with the following messages", validator.IsValid, validator.GetValidationMessagesAsString());
                if (validator.IsValid)
                {
                    var fc = new FiscalCodeBuilder(deserialized);
                    var outputObj = new
                        {result = "success", fiscalCodeInfo = new FiscalCodeJson(fc.ComputedFiscalCode, deserialized)};
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
                var serializedObject = JsonConvert.SerializeObject(fcValidator, Formatting.Indented, new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeHtml });
                return serializedObject;
            }

            var obj = new { result = "failed", payload = validator };
            return JsonConvert.SerializeObject(obj);
        }
    }
}
