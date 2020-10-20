using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ALD.LibFiscalCode.Tests
{
    public class MappingTest:IClassFixture<DbFixture>
    {
        private readonly ServiceProvider serviceProvider;
        private readonly SqlServerDataContext dataContext;

        public MappingTest(DbFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
            dataContext = serviceProvider.GetService<SqlServerDataContext>();
        }
        [Fact]
        public async void TestRegions()
        {
            PlacesController controller = new PlacesController(dataContext);
            var result = await controller.GetProvincesMapping();
            Assert.NotNull(result);
        }

        [Fact]
        public async void TestRegionsOkResult()
        {
            PlacesController controller = new PlacesController(dataContext);
            var result = await controller.GetProvincesMapping();
            Assert.Equal(typeof(OkObjectResult), result.GetType());
        }
    }
}