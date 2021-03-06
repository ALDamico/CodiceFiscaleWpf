﻿using System;
using System.Configuration;
using System.Linq;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Controllers;
using CodiceFiscaleApi.Requests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ALD.LibFiscalCode.Tests
{
    public class ValidationApiTest : IClassFixture<DbFixture>
    {
        private readonly ServiceProvider serviceProvider;
        private readonly AppDataContext dataContext;

        public ValidationApiTest(DbFixture fixture)
        {
            serviceProvider = fixture.ServiceProvider;
            dataContext = serviceProvider.GetService<AppDataContext>();
            _ = dataContext.Places.FirstOrDefault();
        }

        [Fact]
        void InvalidFiscalCodeTest()
        {
            var fiscalCode = "ABCDEF99";
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
            var fiscalCode = "RSSMRA70A01H501S";
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
            Assert.True(result.Outcome);
        }

        [Fact]
        void FiscalCodeNoCheckDigitTest()
        {
            var fiscalCode = "RSSMRA70A01H501";
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
            Assert.False(result.Outcome);
        }
    }
}