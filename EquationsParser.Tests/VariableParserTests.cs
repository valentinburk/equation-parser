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
    internal sealed class VariableParserTests
    {
        private static readonly TestCaseData[] TestCases =
        {
            new TestCaseData("y^2", new Variable('y', 2)),
            new TestCaseData("x", new Variable('x')),
            new TestCaseData("z^0.19", new Variable('z', 0.19m)),
            new TestCaseData("z^-296.993", new Variable('z', -296.993m)),
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
            VariableParser instance = default;

            // Act
            Action action = () => instance = CreateInstance();

            // Assert
            Should.NotThrow(action);
            instance.ShouldNotBe(default);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Test_010_Parse_ShouldBeCorrect(string input, Variable expectedResult)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            var result = instance.Parse(input);

            // Assert
            result.ShouldBe(expectedResult);
        }

        private VariableParser CreateInstance()
        {
            return new VariableParser(_logger);
        }
    }
}
