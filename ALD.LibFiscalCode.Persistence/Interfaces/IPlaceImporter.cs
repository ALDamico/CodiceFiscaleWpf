namespace ALD.LibFiscalCode.Persistence.Interfaces
{
    public interface IPlaceImporter
    {
        string Path { get; }
        IImportStrategy ImportStrategy { get; }
        void ExecuteImport();
    }
}