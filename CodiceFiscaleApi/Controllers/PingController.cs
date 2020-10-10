using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Configuration;
using CodiceFiscaleApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using System.Diagnostics;

namespace CodiceFiscaleApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly SqlServerDataContext dataContext;
        private readonly JsonNetConfiguration jsonNetConfiguration;

        public PingController(SqlServerDataContext dataContext)
        {
            this.dataContext = dataContext;
            jsonNetConfiguration = new JsonNetConfiguration();
            
        }
        [HttpGet]
        public string Get()
        {
            Log.Information("Requested ping from {}", HttpContext.Connection.RemoteIpAddress);
            return "Pong!";
        }

        [HttpGet("databaseStatus")]
        public async Task<DatabaseStatusResponse> GetDatabaseStatus()
        {
            Log.Information("Requested database information from {}", HttpContext.Connection.RemoteIpAddress);
            DatabaseStatusResponse statusResponse = new DatabaseStatusResponse();
            if (dataContext != null)
            {
                statusResponse.DbName = dataContext.Database.GetDbConnection().Database;
                //TODO Come up with a better way. This one is hacky.
                var placeTablesExists =
                    dataContext.Database.ExecuteSqlRaw("UPDATE Places SET name = name");
                statusResponse.PlacesTableExists = placeTablesExists > 0;
                if (placeTablesExists > 0)
                {
                    statusResponse.PlacesCount = await dataContext.Places.CountAsync().ConfigureAwait(false);
                }
                else
                {
                    statusResponse.PlacesCount = 0;
                }

                try
                {
                    statusResponse.LastUpdated = Process.GetProcesses()[0].StartTime;
                }
                catch (Win32Exception)
                {
                    statusResponse.LastUpdated = null;
                }
                
            }

            Log.Information("Response: {0}", statusResponse);
            return statusResponse;
        }
    }
}