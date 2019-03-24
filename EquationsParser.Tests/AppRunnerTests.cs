using System;
using System.Collections.Generic;
using EquationsParser.Contracts;
using EquationsParser.Logic;
using EquationsParser.Models;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace EquationsParser.Tests
{
    [TestFixture]
    internal sealed class AppRunnerTests
    {
        private Config _config;
        private ICalculator _calculator;
        private IEquationsHandler _equationsHandler;
        private Func<Config, IEquationsHandler> _equationsHandlerFactory;
        private ILogger _logger;

        private readonly Dictionary<string, string> _equationsResults = new Dictionary<string, string>
        {
            { "a", "b" },
            { "c", "d" },
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _config = new Config
            {
                InputFilepath = "FakeValidFilename",
                OutputFilepath = "FakeValidFilename",
            };

            _calculator = Substitute.For<ICalculator>();

            _equationsHandler = Substitute.For<IEquationsHandler>();
            _equationsHandler.GetEquations()
                .Returns(_equationsResults.Keys);

            _equationsHandlerFactory = cfg => _equationsHandler;

            _logger = Substitute.For<ILogger>();
        }

        [Test]
        public void Test_000_Should_create_instance()
        {
            // Arrange
            AppRunner instance = default;

            // Act
            Action action = () => instance = CreateInstance();

            // Assert
            Should.NotThrow(action);
            instance.ShouldNotBe(default);
        }

        [Test]
        public void Test_010_RunApp_ShouldWork()
        {
            // Arrange
            _config.ProgramMode = ProgramMode.Interactive;
            var instance = CreateInstance();

            // Act
            Action action = async () => await instance.RunAppAsync();

            // Assert
            Should.NotThrow(action);
            _calculator.Received(requiredNumberOfCalls: _equationsResults.Count)
                .Calculate(Arg.Any<string>());
            _equationsHandler.Received(requiredNumberOfCalls: _equationsResults.Count)
                .OutputResultAsync(Arg.Any<string>());
        }

        private AppRunner CreateInstance() =>
            new AppRunner(_config, _calculator, _equationsHandlerFactory, _logger);
    }
}
