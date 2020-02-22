using ALD.LibFiscalCode.Persistence.Interfaces;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public class PlacesImporter:IPlaceImporter
    {
        public PlacesImporter(string filename, IImportStrategy strategy)
        {
            //Required for compatibility with .NET Core
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Path = filename;
            ImportStrategy = strategy;
            ExecuteImport();
        }
        public string Path { get; }
        public IImportStrategy ImportStrategy { get; }
        
        public void ExecuteImport()
        {
            ImportStrategy.ImportData(Path);
        }
    }
}