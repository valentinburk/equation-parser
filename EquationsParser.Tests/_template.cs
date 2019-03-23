//using EquationsParser.Contracts;
//using EquationsParser.Logic;
//using NUnit.Framework;
//using Shouldly;
//using System;
//using NSubstitute;

//namespace EquationsParser.Tests
//{
//    [TestFixture]
//    internal sealed class _RunnerTests
//    {
//        private ICalculator _calculator;
//        private IFileProcessor _fileProcessor;

//        [OneTimeSetUp]
//        public void OneTimeSetUp()
//        {
//            _calculator = Substitute.For<ICalculator>();
//            _fileProcessor = Substitute.For<IFileProcessor>();
//        }

//        [Test]
//        public void Test_000_Should_create_instance()
//        {
//            // Arrange
//            Runner instance = default;

//            // Act
//            Action action = () => instance = CreateInstance();

//            // Assert
//            Should.NotThrow(action);
//            instance.ShouldNotBe(default);
//        }

//        [Test]
//        public void Test_010_Name()
//        {
//            // Arrange
//            var instance = CreateInstance();

//            // Act

//            // Assert
//        }

//        [Test]
//        public void Test_020_Name()
//        {
//            // Arrange
//            var instance = CreateInstance();

//            // Act

//            // Assert
//        }

//        private Runner CreateInstance()
//        {
//            return new Runner(_calculator, _fileProcessor);
//        }
//    }
//}
