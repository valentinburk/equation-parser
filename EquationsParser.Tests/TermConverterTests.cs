using EquationsParser.Logic;
using EquationsParser.Models;
using NUnit.Framework;
using Shouldly;
using System;
using EquationsParser.Contracts;
using NSubstitute;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class TermConverterTests
    {
        private static readonly TestCaseData[] TestCases =
        {
            new TestCaseData(new Term { Multiplier = 1m, Variables = new[] { new Variable('x', 2),  } }, "+x^2"),
            new TestCaseData(new Term { Multiplier = 3.5m, Variables = new[] { new Variable('x'), new Variable('y') } }, "+3.5xy"),
            new TestCaseData(new Term { Multiplier = -1m, Variables = new[] { new Variable('y') } }, "-y"),
            new TestCaseData(new Term { Multiplier = -58m, Variables = new[] { new Variable('x', 2) } }, "-58x^2"),
            new TestCaseData(new Term { Multiplier = -1m, Variables = new[] { new Variable('x'), new Variable('y') } }, "-xy"),
            new TestCaseData(new Term { Multiplier = 4m, Variables = new Variable[0] }, "+4"),
        };

        private ILogger _logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = Substitute.For<ILogger>();
        }

        [Test]
        public void Test_000_Should_create_instance()
        {
            // Arrange
            TermConverter instance = default;

            // Act
            Action action = () => instance = CreateInstance();

            // Assert
            Should.NotThrow(action);
            instance.ShouldNotBe(default);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Test_010_ToCanonical_ShouldBeCorrect(Term input, string expectedResult)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            var result = instance.ToCanonical(input);

            // Assert
            result.ShouldBe(expectedResult);
        }

        private TermConverter CreateInstance() =>
            new TermConverter(_logger);
    }
}
