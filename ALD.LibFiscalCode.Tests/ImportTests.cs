using ALD.LibFiscalCode.Persistence.Importer;
using System;
using Xunit;

namespace ALD.LibFiscalCode.Tests
{
    public class ImportTests
    {
        [Fact]
        public void CsvTest()
        {
            var importer = new PlacesImporter("C:/Users/aldam/Downloads/Elenco-codici-statistici-e-denominazioni-delle-unita-territoriali/Elenco codici statistici e denominazioni delle unita territoriali/Elenco-codici-statistici-e-denominazioni-al-01_01_2020.csv", new CsvImportStrategy());
        }
    }
}