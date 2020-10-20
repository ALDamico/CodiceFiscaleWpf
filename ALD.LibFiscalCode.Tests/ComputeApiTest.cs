using System;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Controllers;
using CodiceFiscaleApi.Requests;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks;
using Xunit;

namespace ALD.LibFiscalCode.Tests
{
    public class ComputeApiTest : IClassFixture<DbFixture>
    {
        private readonly ServiceProvider serviceProvider;
        private readonly SqlServerDataContext dataContext;

        public ComputeApiTest(DbFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
            dataContext = serviceProvider.GetService<SqlServerDataContext>();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.WithProperty("App Name", "CodFiscale.Online API")
                .CreateLogger();
            Log.Information($"Started application on {DateTime.Now}");
        }

        [Fact]
        async void CalculateTest()
        {
            var controller = new FiscalCodeController(dataContext);
            var request = new PersonRequest()
            {
                Name = "Mario",
                Surname = "Rossi",
                BirthDate = new DateTime(1970, 1, 1),
                BirthPlaceId = (from p in dataContext.Places
                    where p.Name == "Roma"
                          && p.StartDate <= new DateTime(1970, 1, 1)
                          && p.EndDate >= new DateTime(1970, 1, 1)
                    select p.Id).Single(),
                Gender = "Male"
            };
            
            var result = await controller.Calculate(JsonConvert.SerializeObject(request));
            
            //Assert.NotNull(result);
            //Assert.NotNull(result.FiscalCode);
            Assert.Equal("RSSMRA70A01H501S", result.FiscalCode.FiscalCode);
        }
    }
}