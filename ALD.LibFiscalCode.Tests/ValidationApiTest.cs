using System;
using System.Configuration;
using System.Linq;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Controllers;
using CodiceFiscaleApi.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ALD.LibFiscalCode.Tests
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<AppDataContext>(options => options.UseSqlServer("Server=localhost;Database=fiscal_code;Trusted_Connection=True;"),
                    ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
    public class ValidationApiTest:IClassFixture<DbFixture>
    {
        private readonly ServiceProvider serviceProvider;

        public ValidationApiTest(DbFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
        }
        [Fact]
        void InvalidFiscalCodeTest()
        {
            var fiscalCode = "ABCDEF99";
            var dataContext = serviceProvider.GetService<AppDataContext>();
            var controller = new FiscalCodeController(dataContext);
            var validationRequest = new ValidationRequest();
            validationRequest.Gender = "Male";
            validationRequest.Name = "Mario";
            validationRequest.Surname = "Rossi";
            validationRequest.BirthDate = new DateTime(1970, 1, 1);
            validationRequest.BirthPlaceId =
                (from p in dataContext.Places
                where p.Name == "Roma"
                      && p.StartDate <= validationRequest.BirthDate
                      && p.EndDate >= validationRequest.BirthDate
                select p.Id).Single();
            validationRequest.FiscalCode = fiscalCode;
            var result = controller.ValidateFiscalCode(validationRequest);
            Assert.True(result.Outcome == false);
        }

        [Fact]
        void ValidFiscalCodeTest()
        {
            var fiscalCode = "RSSMRA70A01H501";
            var dataContext = serviceProvider.GetService<AppDataContext>();
            var controller = new FiscalCodeController(dataContext);
            var validationRequest = new ValidationRequest();
            validationRequest.Gender = "Male";
            validationRequest.Name = "Mario";
            validationRequest.Surname = "Rossi";
            validationRequest.BirthDate = new DateTime(1970, 1, 1);
            validationRequest.BirthPlaceId =
                (from p in dataContext.Places
                    where p.Name == "Roma"
                          && p.StartDate <= validationRequest.BirthDate
                          && p.EndDate >= validationRequest.BirthDate
                    select p.Id).Single();
            validationRequest.FiscalCode = fiscalCode;
            var result = controller.ValidateFiscalCode(validationRequest);
            Assert.True(result.Outcome == true);
        }
    }
}