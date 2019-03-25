using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using EquationsParser.Contracts;
using EquationsParser.Exceptions;
using EquationsParser.Models;

namespace EquationsParser.Logic
{
    internal sealed class AppRunner : IAppRunner, IDisposable
    {
        private readonly ICalculator _calculator;
        private readonly IEquationsHandler _equationsHandler;
        private readonly ILogger _logger;

        private readonly BlockingCollection<string> _equationsToProcess;

        public AppRunner(
            Config config,
            ICalculator calculator,
            Func<Config, IEquationsHandler> equationsHandlerFactory,
            ILogger logger)
        {
            EnsureArg.IsNotNull(config, nameof(config));
            EnsureArg.IsNotNull(calculator, nameof(calculator));
            EnsureArg.IsNotNull(equationsHandlerFactory, nameof(equationsHandlerFactory));
            EnsureArg.IsNotNull(logger, nameof(logger));

            _calculator = calculator;
            _equationsHandler = equationsHandlerFactory(config);
            _logger = logger;

            _equationsToProcess = new BlockingCollection<string>();
        }

        public Task RunAppAsync(CancellationToken cancellationToken = default)
        {
            _ = Task.Run(() =>
            {
                foreach (var equation in _equationsHandler.GetEquations(cancellationToken))
                {
                    _equationsToProcess.Add(equation, CancellationToken.None);
                }

                _equationsToProcess.CompleteAdding();
            }, CancellationToken.None);

            return ProcessEquationsAsync();
        }

        private Task ProcessEquationsAsync()
        {
            return Task.Run(async () =>
            {
                foreach (var equation in _equationsToProcess.GetConsumingEnumerable())
                {
                    try
                    {
                        var result = _calculator.Calculate(equation);
                        await _equationsHandler.OutputResultAsync(result);
                    }
                    catch (InvalidEquationException e)
                    {
                        _logger.Log(
                            TraceLevel.Warning,
                            $"Equation parsing operation failed while processing {equation} ({e.Message})");
                    }
                    catch (Exception e)
                    {
                        _logger.Log(
                            TraceLevel.Error,
                            $"Unknown error: {e.Message}");
                    }

                    if (_equationsToProcess.IsCompleted &&
                        _equationsToProcess.IsAddingCompleted)
                    {
                        break;
                    }
                }
            });
        }

        public void Dispose()
        {
            _equationsHandler?.Dispose();
            _equationsToProcess?.Dispose();
        }
    }
}
