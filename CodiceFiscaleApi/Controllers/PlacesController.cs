using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM;
using ALD.LibFiscalCode.Persistence.ORM.MySQL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodiceFiscaleApi
{
    [Route("api/[controller]")]
    public class PlacesController : Controller
    {
        private AppDataContext dataContext;

        public PlacesController(AppDataContextBase dataContext)
        {
            this.dataContext = dataContext as AppDataContext;
        }

        // GET: api/<controller>
    [HttpGet]
        public string Get(string name)
        {
            
            if (!string.IsNullOrEmpty(name) && name.Length < 3)
            {
                return null;
            }

            
            var matchingPlaces = dataContext.Places.Where(p => p.Name.Contains(name));
            var outputObject = JsonConvert.SerializeObject(matchingPlaces, Formatting.Indented);
            if (matchingPlaces.Count() == 1)
            {
                outputObject = JsonConvert.SerializeObject(matchingPlaces.First(), Formatting.Indented);
            }
            return outputObject;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}