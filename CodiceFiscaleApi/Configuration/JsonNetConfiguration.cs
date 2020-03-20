using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodiceFiscaleApi.Configuration
{
    public class JsonNetConfiguration
    {
       public JsonNetConfiguration()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = ContractResolver,
                Formatting = Formatting.Indented
            };
        }
        public DefaultContractResolver ContractResolver { get; }
        public JsonSerializerSettings SerializerSettings { get; }
    }
}
