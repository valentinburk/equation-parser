using EquationsParser.Models;
using Xunit;

namespace EquationsParser.Tests
{
    public class TermTests
    {
        [Fact]
        public void CanConvertFromString()
        {
            string term1 = "-3.5xy^2";
            string term2 = "+8x^9y^2";
            string term3 = "12";
            string term4 = "-x";
            string term5 = "yx";

            Term result1 = Term.FromString(term1);
            Term result2 = Term.FromString(term2);
            Term result3 = Term.FromString(term3);
            Term result4 = Term.FromString(term4);
            Term result5 = Term.FromString(term5);

            Assert.Equal(-3.5m, result1.Multiplier);
            Assert.Contains("x", result1.Variables);
            Assert.Contains("y^2", result1.Variables);
            Assert.Equal(8m, result2.Multiplier);
            Assert.Contains("x^9", result2.Variables);
            Assert.Contains("y^2", result2.Variables);
            Assert.Equal(12, result3.Multiplier);
            Assert.Empty(result3.Variables);
            Assert.Equal(-1, result4.Multiplier);
            Assert.Contains("x", result4.Variables);
            Assert.Equal(1, result5.Multiplier);
            Assert.Contains("y", result5.Variables);
            Assert.Contains("x", result5.Variables);
        }
    }
}
