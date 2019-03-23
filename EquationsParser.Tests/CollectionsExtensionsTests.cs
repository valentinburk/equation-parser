using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class CollectionsExtensionsTests
    {
        private static readonly TestCaseData[] PositiveTestCases =
        {
            new TestCaseData(new[] { "a", "b", "c" }, new[] { "a", "b", "c" }),
            new TestCaseData(new[] { "a", "b", "c" }, new[] { "c", "a", "b" }),
        };

        private static readonly TestCaseData[] NegativeTestCases =
        {
            new TestCaseData(new[] { "a", "b", "c" }, new[] { "a", "b", "b" }),
            new TestCaseData(new[] { "a", "b", "c" }, new[] { "c", "a" }),
        };

        [Test]
        [TestCase(10)]
        public void Test_010_Add_TrimmedAndNot_ShouldBeOk(int count)
        {
            // Arrange
            const string contentDirty = "     Just some random content ";
            const string contentTrimmed = "Just some random content";

            var list = new List<string>();

            var stringBuilders = new List<StringBuilder>();
            for (var i = 0; i < count; i++)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(i % 2 == 0 ? contentDirty : contentTrimmed);
                stringBuilders.Add(stringBuilder);
            }

            // Act
            foreach (var stringBuilder in stringBuilders)
            {
                list.Add(stringBuilder);
            }

            // Assert
            list.Count.ShouldBe(count);
            list.All(e => e == contentTrimmed).ShouldBeTrue();
        }

        [Test]
        [TestCaseSource(nameof(PositiveTestCases))]
        public void Test_110_EqualsInside_Equals_ShouldBeTrue(string[] first, string[] second)
        {
            // Arrange & Act
            var result = first.EqualsInside(second);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        [TestCaseSource(nameof(NegativeTestCases))]
        public void Test_120_EqualsInside_NotEquals_ShouldBeFalse(string[] first, string[] second)
        {
            // Arrange & Act
            var result = first.EqualsInside(second);

            // Assert
            result.ShouldBeFalse();
        }
    }
}
