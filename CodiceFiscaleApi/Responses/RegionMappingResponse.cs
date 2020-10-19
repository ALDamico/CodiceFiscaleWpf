using System;
using System.Collections.Generic;

namespace CodiceFiscaleApi.Responses
{
    public class RegionMappingResponse
    {
        public RegionMappingResponse()
        {
            Payload = new Dictionary<string, List<ProvinceResponse>>();
        }
        public Dictionary<String, List<ProvinceResponse>> Payload { get; set; }
    }
}