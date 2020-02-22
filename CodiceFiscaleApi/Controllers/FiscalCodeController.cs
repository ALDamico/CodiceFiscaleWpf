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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodiceFiscaleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiscalCodeController : ControllerBase
    {
        private AppDataContext dataContext;

        public FiscalCodeController(AppDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        // GET: api/FiscalCode
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FiscalCode/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FiscalCode
        [HttpPost("calculate")]
        public string Post([FromForm] Person person,[FromForm] int placeOfBirthId)
        {
            if (person != null)
            {
                person.PlaceOfBirth = dataContext.Places.SingleOrDefault(p => p.Id == placeOfBirthId);
            }
            var validator = new PersonValidator(person);
            if (validator.IsValid)
            {
                var fc = new FiscalCodeBuilder(person);
                var serializedObject = JsonConvert.SerializeObject(fc.ComputedFiscalCode, Formatting.Indented);
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
                person.PlaceOfBirth = dataContext.Places.Where(p => p.Id == placeOfBirthId).SingleOrDefault();
            }
            var validator = new PersonValidator(person);
            if (validator.IsValid)
            {
                var fc = new FiscalCodeBuilder(fiscalCode);
                var fcValidator = new FiscalCodeValidator(person, fc.ComputedFiscalCode);
                var serializedObject = JsonConvert.SerializeObject(fcValidator, Formatting.Indented, new JsonSerializerSettings() {StringEscapeHandling = StringEscapeHandling.EscapeHtml });
                return serializedObject;
            }

            var obj = new { result = "failed", payload = validator };
            return JsonConvert.SerializeObject(obj);
        }


    }
}
