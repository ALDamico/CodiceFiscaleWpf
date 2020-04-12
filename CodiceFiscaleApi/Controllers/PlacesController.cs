using System;
using System.Linq;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodiceFiscaleApi.Controllers
{
    [Route("[controller]")]
    public class PlacesController : Controller
    {
        private readonly AppDataContext dataContext;
        private readonly JsonNetConfiguration jsonNetConfiguration;

        public PlacesController(AppDataContext dataContext)
        {
            this.dataContext = dataContext;
            jsonNetConfiguration = new JsonNetConfiguration();
            
        }

        [HttpGet("all")]
        public string GetAllPlaces()
        {
            Log.Information("Requested all places from {0}", this.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);
            
            var placesList = (
                from place
                in dataContext.Places
                select place
                );
            var outputObject = JsonConvert.SerializeObject(placesList, jsonNetConfiguration.SerializerSettings);
            return outputObject;
        }

        // GET: api/<controller>
        [HttpGet]
        public string Get(string name, DateTime? validOn = null)
        {
            Log.Information("Requested place with partial name {0} valid on {1} from {2}", name, validOn != null ? validOn.ToString(): "forever", HttpContext.Connection.RemoteIpAddress);
            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                return null;
            }

            var matchingPlaces = dataContext.Places.Where(p => p.Name.Contains(name));
            if (validOn != null)
            {
                matchingPlaces = matchingPlaces.Where(p => (p.StartDate == null || p.StartDate >= validOn) || (p.EndDate == null || p.EndDate <= validOn));
            }
            else
            {
                matchingPlaces = matchingPlaces.Where(p => p.EndDate == null);
            }

            var outputObject = JsonConvert.SerializeObject(matchingPlaces, jsonNetConfiguration.SerializerSettings);
            
            return outputObject;
        } 


        [HttpGet("{id}")]
        public string Get(int id)
        {
            Log.Information("Requested place with id {0} from {1}", id, this.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);
            string outputObject = null;
            try
            {
                var place = dataContext.Places.SingleOrDefault(p => p.Id == id);
                outputObject = JsonConvert.SerializeObject(place, Formatting.Indented);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
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