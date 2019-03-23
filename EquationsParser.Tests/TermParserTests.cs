using EquationsParser.Contracts;
using EquationsParser.Logic;
using NUnit.Framework;
using Shouldly;
using System;
using EquationsParser.Models;
using NSubstitute;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class TermParserTests
    {
        private static readonly Variable FakeValidVariable = new Variable('x');
        private static readonly TestCaseData[] TestCases =
        {
            new TestCaseData("x^2", new Term { Multiplier = 1m, Variables = new[] { FakeValidVariable } }),
            new TestCaseData("3.5xy", new Term { Multiplier = 3.5m, Variables = new[] { FakeValidVariable, FakeValidVariable } }),
            new TestCaseData("-y", new Term { Multiplier = -1m, Variables = new[] { FakeValidVariable } }),
            new TestCaseData("-58x^2", new Term { Multiplier = -58m, Variables = new[] { FakeValidVariable } }),
            new TestCaseData("-xy", new Term { Multiplier = -1m, Variables = new[] { FakeValidVariable, FakeValidVariable } }),
            new TestCaseData("4", new Term { Multiplier = 4m, Variables = new Variable[0] }),
        };

        private IVariableParser _variableParser;
        private ILogger _logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _variableParser = Substitute.For<IVariableParser>();
            _variableParser.Parse(Arg.Any<string>())
                .Returns(FakeValidVariable);

            _logger = Substitute.For<ILogger>();
        }

        [Test]
        public void Test_000_Should_create_instance()
        {
            // Arrange
            TermParser instance = default;

            // Act
            Action action = () => instance = CreateInstance();

            // Assert
            Should.NotThrow(action);
            instance.ShouldNotBe(default);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Test_010_Parse_ShouldBeCorrect(string input, Term expectedResult)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            var result = instance.Parse(input);

            // Assert
            result.ShouldBe(expectedResult);
        }

        private TermParser CreateInstance()
        {
            return new TermParser(_variableParser, _logger);
        }
    }
}
