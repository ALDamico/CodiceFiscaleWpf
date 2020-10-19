using System.Collections.Generic;
using ALD.LibFiscalCode.Persistence.Models;
using CodiceFiscaleApi.Responses;

namespace CodiceFiscaleApi.Converters
{
    public static class RegionResponseConverter
    {
        public static RegionMappingResponse ToResponse(List<RegionMapping> entities)
        {
            RegionMappingResponse response = new RegionMappingResponse();
            foreach (var entity in entities)
            {
                response.Payload[entity.Name] = new List<ProvinceResponse>();
                var provinces = entity.Provinces;
                foreach (var provinceEntity in provinces)
                {
                    var provinceResponse = ProvinceResponseConverter.ToResponse(provinceEntity);
                    response.Payload[entity.Name].Add(provinceResponse);
                }   
            }
            return response;
        }
    }
}