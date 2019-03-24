using EquationsParser.Contracts;
using EquationsParser.Exceptions;
using EquationsParser.Logic;
using EquationsParser.Models;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class CalculatorTests
    {
        private static readonly string FakeValidString = nameof(FakeValidString);
        private static readonly Term FakeValidTerm = new Term(Array.Empty<Variable>());

        private static readonly TestCaseData[] InvalidEquations =
        {
            new TestCaseData(null),
            new TestCaseData(""),
            new TestCaseData("x"),
            new TestCaseData("x++1=y"),
            new TestCaseData("x+1"),
        };

        private ILogger _logger;
        private IStringParser _stringParser;
        private ITermParser _termParser;
        private ITermConverter _termConverter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = Substitute.For<ILogger>();

            _stringParser = Substitute.For<IStringParser>();
            _stringParser.Parse(Arg.Any<string>())
                .Returns(new[] { FakeValidString, FakeValidString });

            _termParser = Substitute.For<ITermParser>();
            _termParser.Parse(Arg.Any<string>())
                .Returns(FakeValidTerm);

            _termConverter = Substitute.For<ITermConverter>();
            _termConverter.ToCanonical(Arg.Any<Term>())
                .Returns(FakeValidString);
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
        public void Test_010_Calculate_CorrectExpression_ShouldWork()
        {
            const string equation = "x=y^2";

            // Arrange
            var instance = CreateInstance();

            // Act
            var actualResult = instance.Calculate(equation);

            // Assert
            actualResult.ShouldNotBeNull();
            actualResult.Length.ShouldBeGreaterThan(0);
        }

        [Test]
        [TestCaseSource(nameof(InvalidEquations))]
        public void Test_010_Calculate_InvalidExpression_ShouldThrow(string equation)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            Action action = () => instance.Calculate(equation);

            // Assert
            action.ShouldThrow<InvalidEquationException>();
        }

        private Calculator CreateInstance() =>
            new Calculator(_stringParser, _termParser, _termConverter, _logger);
    }
}
