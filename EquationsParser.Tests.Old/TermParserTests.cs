using Xunit;

namespace EquationsParser.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void CanCalculate()
        {
            string input = "-58x^2+3.5xy-y=-58x^2-xy+y+4";
            string output = "59x^2+4.5xy-2y-4=0";

            Calculator calculator = new Calculator();
            var result = calculator.Calculate(input);

            Assert.Equal(output, result);
        }
    }
}
