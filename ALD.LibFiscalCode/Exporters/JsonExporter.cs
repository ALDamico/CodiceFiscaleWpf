using System;
using System.IO;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using ALD.LibFiscalCode.Interfaces;
using Newtonsoft.Json;

namespace ALD.LibFiscalCode.Exporters
{
    public class JsonExporter:IExportStrategy
    {
        public void Export(MainViewModel viewModel, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            var exportedObject = new PersonJson(viewModel.CurrentPerson, viewModel.FiscalCode);
            var outputJson = JsonConvert.SerializeObject(exportedObject, Formatting.Indented);
            File.WriteAllLines(fileName, new[] { outputJson });
            
        }
    }
}