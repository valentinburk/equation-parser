using EquationsParser.Contracts;
using EquationsParser.Logic;
using NUnit.Framework;
using Shouldly;
using System;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class CalculatorTests
    {
        private static TestCaseData[] _testCases =
        {
            new TestCaseData("x^2+3.5xy-y=-58x^2-xy+y+4", "59x^2+4.5xy-2y-4=0"), 
            //new TestCaseData("", ""), 
            //new TestCaseData("", ""), 
            //new TestCaseData("", ""), 
            //new TestCaseData("", ""), 
        };

        private IStringParser _stringParser;
        private ITermParser _termParser;
        private ITermConverter _termConverter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _stringParser = new StringParser();
            _termParser = new TermParser();
            _termConverter = new TermConverter();
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
        [TestCaseSource(nameof(_testCases))]
        public void Test_010_Calculate_ShouldReturnCorrectValue(string equation, string expectedResult)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            var actualResult = instance.Calculate(equation);

            // Assert
            actualResult.ShouldBe(expectedResult);
        }

        private Calculator CreateInstance()
        {
            return new Calculator(_stringParser, _termParser, _termConverter);
        }
    }
}
