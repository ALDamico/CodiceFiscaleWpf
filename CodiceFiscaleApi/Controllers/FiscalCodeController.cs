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

namespace CodiceFiscaleApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FiscalCodeController : ControllerBase
    {
        private AppDataContext dataContext;
        private JsonNetConfiguration jsonNetConfiguration;

        public FiscalCodeController(AppDataContext dataContext)
        {
            this.dataContext = dataContext;
            jsonNetConfiguration = new JsonNetConfiguration();
        }

        // POST: api/FiscalCode
        [HttpPost("calculate")]
        public string Post([FromForm] string person, [FromForm] int? placeOfBirthId)
        {
            Person deserialized = new Person();
            deserialized = JsonConvert.DeserializeObject(person, deserialized.GetType(), jsonNetConfiguration.SerializerSettings) as Person;
            if (person != null)
            {
                deserialized.PlaceOfBirth = dataContext.Places.SingleOrDefault(p => p.Id == placeOfBirthId);
            }
            var validator = new PersonValidator(deserialized);
            if (validator.IsValid)
            {
                var fc = new FiscalCodeBuilder(deserialized);
                var outputObj = new { result= "success", fiscalCodeInfo = new FiscalCodeJson(fc.ComputedFiscalCode, deserialized) };
                var serializedObject = JsonConvert.SerializeObject(outputObj, jsonNetConfiguration.SerializerSettings);
                return serializedObject;
            }
            var obj = new { result = "failed", payload = validator };
            return JsonConvert.SerializeObject(obj);
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
