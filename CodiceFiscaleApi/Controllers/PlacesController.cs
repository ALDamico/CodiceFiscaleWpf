using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Importer;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodiceFiscaleApi.Controllers
{
    [Route("[controller]")]
    public class PlacesController : Controller
    {
        private readonly SqlServerDataContext dataContext;
        private readonly JsonNetConfiguration jsonNetConfiguration;

        public PlacesController(SqlServerDataContext dataContext)
        {
            this.dataContext = dataContext;
            jsonNetConfiguration = new JsonNetConfiguration();
            
        }

        [HttpGet("all")]
        public List<Place> GetAllPlaces()
        {
            Log.Information("Requested all places from {0}", HttpContext.Connection.RemoteIpAddress);
            
            var placesList = (
                from place
                in dataContext.Places
                select place
                );
            
            return placesList.ToList();
        }

        // GET: api/<controller>
        [HttpGet]
        public List<Place> Get(string name, DateTime? validOn = null)
        {
            //This allows us to get around collation mismatches
            var nameUpper = name.ToUpper();
            Log.Information("Requested place with partial name {0} valid on {1} from {2}", name, validOn != null ? validOn.ToString(): "forever", HttpContext.Connection.RemoteIpAddress);
            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                return null;
            }

            var matchingPlaces = dataContext.Places.Where(p => p.Name.Contains(nameUpper));
            if (validOn != null)
            {
                matchingPlaces = matchingPlaces.Where(p => (p.StartDate == null || p.StartDate <= validOn) && (p.EndDate == null || p.EndDate >= validOn));
            }
            else
            {
                matchingPlaces = matchingPlaces.Where(p => p.EndDate == null);
            }

            return matchingPlaces.ToList();
        } 


        [HttpGet("{id}")]
        public Place Get(int id)
        {
            Log.Information("Requested place with id {0} from {1}", id, HttpContext.Connection.RemoteIpAddress);
            Place place = null;
            try
            {
                place = dataContext.Places.SingleOrDefault(p => p.Id == id);
            }
            catch (SqlException ex)
            {
                Log.Error(ex.ToString());
            }
            
            return place;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPost("upload")]
        public async Task UpdateList( IFormFile inputFile)
        {
            if (inputFile == null)
            {
                return;
            }
            if (inputFile.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(filePath))
                {
                    await inputFile.CopyToAsync(stream);
                    
                    var provider = new FileExtensionContentTypeProvider();
                    
                    if (inputFile.ContentType == "text/csv")
                    {
                        PlacesImporter importer = new PlacesImporter(filePath, new CsvImportStrategy(), 2020);
                        
                        
                        
                        return;
                    }
                }
            }
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