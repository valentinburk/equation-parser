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
    internal sealed class AppRunner : IAppRunner
    {
        private readonly Config _config;
        private readonly ICalculator _calculator;
        private readonly Func<Config, IEquationsHandler> _equationsHandlerFactory;
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

            _config = config;
            _calculator = calculator;
            _equationsHandlerFactory = equationsHandlerFactory;
            _logger = logger;

            _equationsToProcess = new BlockingCollection<string>();
        }

        public Task RunAppAsync(CancellationToken cancellationToken = default)
        {
            var equationsHandler = _equationsHandlerFactory(_config);

            _ = Task.Run(() =>
            {
                foreach (var equation in equationsHandler.GetEquations(cancellationToken))
                {
                    _equationsToProcess.Add(equation, CancellationToken.None);
                }

                _equationsToProcess.CompleteAdding();
            }, CancellationToken.None);

            return ProcessEquationsAsync(equationsHandler);
        }

        private Task ProcessEquationsAsync(IEquationsHandler equationsHandler)
        {
            return Task.Run(async () =>
            {
                using (equationsHandler)
                {
                    foreach (var equation in _equationsToProcess.GetConsumingEnumerable())
                    {
                        try
                        {
                            var result = _calculator.Calculate(equation);
                            await equationsHandler.OutputResultAsync(result);
                        }
                        catch (InvalidEquationException e)
                        {
                            _logger.Log(
                                TraceLevel.Error,
                                $"Equation parsing operation failed while processing {equation} ({e.Message})");
                        }

                        if (_equationsToProcess.IsCompleted &&
                            _equationsToProcess.IsAddingCompleted)
                        {
                            break;
                        }
                    }
                }
            });
        }
    }
}
