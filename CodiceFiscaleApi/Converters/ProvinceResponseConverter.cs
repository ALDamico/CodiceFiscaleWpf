using ALD.LibFiscalCode.Persistence.Models;
using CodiceFiscaleApi.Responses;

namespace CodiceFiscaleApi.Converters
{
    public static class ProvinceResponseConverter
    {
        public static ProvinceResponse ToResponse(ProvinceMapping entity)
        {
            var response = new ProvinceResponse();
            response.Abbreviation = entity.Abbreviation;
            response.Name = entity.Name;
            return response;
        }
    }
}