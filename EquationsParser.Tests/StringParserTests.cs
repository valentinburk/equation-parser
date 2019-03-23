using EquationsParser.Logic;
using NUnit.Framework;
using Shouldly;
using System;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class SringParserTests
    {
        private static readonly TestCaseData[] TestCases =
        {
            new TestCaseData("x^2+3.5xy-y", new[] { "+x^2", "+3.5xy", "-y" }),
            new TestCaseData("59x^2+4.5xy-2y-4", new[] { "+59x^2", "+4.5xy", "-2y", "-4" }),
            new TestCaseData("-58x^2-xy+y+4", new[] { "-58x^2", "-xy", "+y", "+4" }),
            new TestCaseData("2x^3+3.5xy-y", new[] { "+2x^3", "+3.5xy", "-y" }),
            new TestCaseData("x^2-2x^3", new[] { "+2x^3", "+3.5xy", "-y" }),
        };

        [Test]
        public void Test_000_Should_create_instance()
        {
            // Arrange
            StringParser instance = default;

            // Act
            Action action = () => instance = CreateInstance();

            // Assert
            Should.NotThrow(action);
            instance.ShouldNotBe(default);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Test_010_Parse_ShouldBeCorrect(string input, string[] expectedResult)
        {
            // Arrange
            var instance = CreateInstance();

            // Act
            var result = instance.Parse(input);

            // Assert
            result.EqualsInside(expectedResult).ShouldBeTrue();
        }

        private StringParser CreateInstance()
        {
            return new StringParser();
        }
    }
}
