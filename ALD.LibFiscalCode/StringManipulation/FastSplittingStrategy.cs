using System.Globalization;
using System.Text;

namespace ALD.LibFiscalCode.StringManipulation
{
    public class FastSplittingStrategy : ISplittingStrategy
    {
        public FastSplittingStrategy(string targetString)
        {
            TargetString = targetString;
            Convert();
        }

        public string Result { get; private set; }
        public string TargetString { get; }

        //Based on https://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net
        public void Convert()
        {
            var normalizedString = TargetString.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            Result = stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToUpperInvariant();
        }
    }
}