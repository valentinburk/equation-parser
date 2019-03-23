using EquationsParser.Contracts;
using EquationsParser.Logic;
using NUnit.Framework;
using Shouldly;
using System;
using EquationsParser.Exceptions;
using NSubstitute;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class CalculatorTests
    {
        private static readonly TestCaseData[] PositiveTestCases =
        {
            new TestCaseData("x^2+3.5xy-y=-58x^2-xy+y+4", "59x^2+4.5xy-2y-4=0"),
            new TestCaseData("x^2+3.5xy-y=2x^3+3.5xy-y", "-2x^3+x^2=0"),
            new TestCaseData("3.5x^9=6y^2-7z", "3.5x^9-6y^2+7z=0"),
            new TestCaseData("19y^7x+6y=-y^7x-0.12", "20y^7x+0.12+6y=0"),
            new TestCaseData("x^2ay^3bz^4c=x^2ay^3bz^4c", "0=0"),
            new TestCaseData("x^2ay^3bz^4c+2x=x^2ay^3bz^4c-2x", "4x=0"),
        };

        private static readonly TestCaseData[] NegativeTestCases =
        {
            new TestCaseData("x=0", "x=0"), // Already converted. Should return same result
            new TestCaseData("16x^2+y+4z=0", "16x^2+4z+y=0"), // Already converted. Should return same result
            new TestCaseData("x^2y=-yx^2", "2x^2y=0"), // Variables on different order. Should work correctly
            new TestCaseData("x^2yz^8o^3=-5yo^3x^2z^8", "6x^2yz^8o^3=0"), // Variables int different order. Should work correctly
            new TestCaseData("x^2ay^3bz^4c+2x=y^3z^4x^2abc-2x", "4x=0"), // Variables int different order. Should work correctly
            new TestCaseData("3.5x^3.5=y^1.96", "3.5x^3.5-y^1.96=0"), // Floatint point number in power. Should work correctly
        };

        private static readonly TestCaseData[] InvalidTestCases =
        {
            new TestCaseData("x"),
            new TestCaseData("x--y=y^2"),
            new TestCaseData("x-3y=12y^7+"),
            new TestCaseData("x+-y=y^2++y"),
            new TestCaseData("x-+y=y^2--y"),
            new TestCaseData("x+="),
            new TestCaseData("x+=12"),
        };

        private ILogger _logger;
        private IStringParser _stringParser;
        private ITermParser _termParser;
        private ITermConverter _termConverter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = Substitute.For<ILogger>();

            _stringParser = new StringParser(_logger);
            _termParser = new TermParser(_logger);
            _termConverter = new TermConverter(_logger);
        }

        [Test]
        public void Test_000_Should_create_instance()
        {
            // Arrange
            Calculator instance = default;

            // Act
            Action action = () => instance = CreateInstance();

            // Assert
            Should.NotThrow(action);
            instance.ShouldNotBe(default);
        }

        [Test]
        [TestCaseSource(nameof(PositiveTestCases))]
        public void Test_010_Calculate_ShouldReturnCorrectValue(string equation, string expectedResult)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            var actualResult = instance.Calculate(equation);

            // Assert
            actualResult.ShouldBe(expectedResult);
        }

        [Test]
        [TestCaseSource(nameof(NegativeTestCases))]
        public void Test_020_Calculate_Negative_ShouldReturnCorrectValue(string equation, string expectedResult)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            var actualResult = instance.Calculate(equation);

            // Assert
            actualResult.ShouldBe(expectedResult);
        }

        [Test]
        [TestCaseSource(nameof(InvalidTestCases))]
        public void Test_030_Calculate_Invalid_ShouldThrow(string equation)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            Action action = () => instance.Calculate(equation);

            // Assert
            Should.Throw<InvalidEquationException>(action);
        }

        private Calculator CreateInstance()
        {
            return new Calculator(_stringParser, _termParser, _termConverter, _logger);
        }
    }
}
