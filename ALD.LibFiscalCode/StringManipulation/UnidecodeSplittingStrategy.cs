using Unidecode.NET;

namespace ALD.LibFiscalCode.StringManipulation
{
    public class UnidecodeSplittingStrategy : ISplittingStrategy
    {
        public string Result { get; private set; }

        public UnidecodeSplittingStrategy(string targetString)
        {
            TargetString = targetString;
            Convert();
        }

        public UnidecodeSplittingStrategy()
        {
            
        }

        public string TargetString
        {
            get => targetString;
            set
            {
                targetString = value;
                Convert();
            }
        }
        private string targetString;

        public void Convert()
        {
            Result = TargetString.ToUpperInvariant().Unidecode();
        }
    }
}