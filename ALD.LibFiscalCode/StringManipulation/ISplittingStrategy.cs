namespace ALD.LibFiscalCode.StringManipulation
{
    public interface ISplittingStrategy
    {
        string TargetString { get; set; }
        string Result { get; }

        void Convert();
    }
}