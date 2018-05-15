using System.Collections.Generic;
using Xunit;

namespace EquationsParser.Tests
{
    public class StringParserTests
    {
        [Fact]
        public void CanParseString()
        {
            string str = "x^2+3.5xy-y=-58x^2-xy+y+4";

            string[] shouldBe = {"+x^2", "+3.5xy", "-y", "-58x^2", "-xy", "+y", "+4"};
            char[] delimiters = { '+', '-' };

            List<string> terms = new List<string>();

            string[] sides = str.Split("=");
            foreach (string side in sides)
            {
                terms.AddRange(StringParser.Parse(side, delimiters));
            }

            Assert.True(terms.EqualsInside(shouldBe));
        }
    }
}
