using ALD.LibFiscalCode.Builders;
using Xunit;
namespace ALD.LibFiscalCode.Tests
{
    public class FiscalCodeBuilderTest
    {
        [Fact]
        void ShortFiscalCodeTest()
        {
            var partialFiscalCode = "ABCDEF99";
            var builder = new FiscalCodeBuilder(partialFiscalCode);
            var result = builder.ComputedFiscalCode;
            Assert.True(result != null);
            Assert.True(result.Surname == "ABC");
            Assert.True(result.Name == "DEF");
            Assert.True(result.DateOfBirthAndGender.Length == 2);
            Assert.True(result.DateOfBirthAndGender == "99");
            Assert.True(string.IsNullOrEmpty(result.PlaceCode));
            Assert.True(string.IsNullOrEmpty(result.CheckDigit));
        }

        [Fact]
        void CorrectFullFiscalCodeTest()
        {
            //Tests validity with a known valid fiscal code
            // RSSMRA70A01H501S
            // Mario Rossi
            // Roma
            // January 1st 1970
            // Male
            var builder = new FiscalCodeBuilder("RSSMRA70A01H501S");
            var result = builder.ComputedFiscalCode;
            Assert.True(result.Surname == "RSS");
            Assert.True(result.Name == "MRA");
            Assert.True(result.DateOfBirthAndGender == "70A01");
            Assert.True(result.PlaceCode == "H501");
            Assert.True(result.CheckDigit == "S");
        }

        [Fact]
        void CorrectFiscalCodeNoCheckDigit()
        {
            //Tests validity with a known valid fiscal code WITHOUT a check digit
            // RSSMRA70A01H501S
            // Mario Rossi
            // Roma
            // January 1st 1970
            // Male
            var builder = new FiscalCodeBuilder("RSSMRA70A01H501");
            var result = builder.ComputedFiscalCode;
            Assert.True(result.Surname == "RSS");
            Assert.True(result.Name == "MRA");
            Assert.True(result.DateOfBirthAndGender == "70A01");
            Assert.True(result.PlaceCode == "H501");
            Assert.True(result.CheckDigit == "S");
        }
    }
}