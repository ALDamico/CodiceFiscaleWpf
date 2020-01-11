namespace ALD.LibFiscalCode.Settings
{
    public interface IChangeSettingsCommand
    {
        bool IsCompleted { get; set; }

        void ChangeSetting();
    }
}