namespace ALD.LibFiscalCode.StringManipulation
{
    public interface ISplittingStrategy
    {
        string TargetString { get; }
        string Result { get; }

        void Convert();
    }
}