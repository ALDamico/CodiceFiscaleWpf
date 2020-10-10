using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using CodiceFiscaleApi.Requests;
using Microsoft.EntityFrameworkCore;

namespace CodiceFiscaleApi.Converters
{
    public class RequestToPersonConverter
    {
        public async Task<Person> ConvertToPersonAsync([NotNull]SqlServerDataContext dataContext, PersonRequest request)
        {
            Person person = new Person();
            person.Name = request.Name;
            person.Surname = request.Surname;
            person.Gender = (Gender) Enum.Parse(typeof(Gender), request.Gender);
            person.DateOfBirth = request.BirthDate;
            
            var placeOfBirth = await dataContext.Places.SingleOrDefaultAsync(p => p.Id == request.BirthPlaceId).ConfigureAwait(false);
            person.PlaceOfBirth = placeOfBirth;
            
            return person;
        }
    }
}