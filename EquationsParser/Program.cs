using EquationsParser.Contracts;
using EquationsParser.Logic;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using EquationsParser.Exceptions;
using EquationsParser.Models;

namespace EquationsParser
{
    internal sealed class Program
    {
        private static Config _config;

        private static ILogger _logger;
        private static ICalculator _calculator;

        private static CancellationTokenSource _cancellationSource;

        static void Main(string[] args)
        {
            _cancellationSource = new CancellationTokenSource();

            Console.CancelKeyPress += (s, a) =>
            {
                a.Cancel = true;
                _cancellationSource.Cancel();
            };

            _config = SetProgramConfig(args);
            _logger = new Logger();
            _calculator = new Calculator(
                new StringParser(_logger),
                new TermParser(new VariableParser(_logger), _logger),
                new TermConverter(_logger),
                _logger);
            
            StartProgram();
        }

        private static void StartProgram()
        {
            IEquationsHandler equationsHandler;

            switch (_config.ProgramMode)
            {
                case ProgramMode.Interactive:
                    equationsHandler = new ConsoleEquationsHandler();
                    break;
                case ProgramMode.FromFile:
                    equationsHandler = new FileEquationsHandler(_config.InputFilepath, _config.OutputFilepath);
                    break;
                default:
                    throw new NotSupportedException($"{_config.ProgramMode} is not supported");
            }

            foreach (var equation in equationsHandler.GetEquations(_cancellationSource.Token))
            {
                try
                {
                    var result = _calculator.Calculate(equation);
                    equationsHandler.OutputResult(result);
                }
                catch (InvalidEquationException e)
                {
                    _logger.Log(
                        TraceLevel.Error,
                        $"Equation parsing operation failed while processing {equation} ({e.Message})");
                }
            }
        }

        private static Config SetProgramConfig(string[] args)
        {
            var mode = ProgramMode.NotChosen;
            string filepath = default;

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-I":
                        mode = ProgramMode.Interactive;
                        break;
                    case "-F":
                        mode = ProgramMode.FromFile;
                        if (args.Length > 1)
                        {
                            filepath = args[1];
                        }
                        break;
                }
            }

            while (mode == ProgramMode.NotChosen)
            {
                Console.WriteLine("Please, choose program mode:");
                Console.WriteLine("I - interactive");
                Console.WriteLine("F - file");

                var key = Console.ReadKey(true);
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.I:
                        mode = ProgramMode.Interactive;
                        break;
                    case ConsoleKey.F:
                        mode = ProgramMode.FromFile;
                        filepath = SetInputFilename(args);
                        break;
                }
            }

            return new Config
            {
                ProgramMode = mode,
                InputFilepath = filepath,
            };
        }

        private static string SetInputFilename(string[] args)
        {
            string filepath = default;

            while (filepath == default)
            {
                Console.WriteLine("Please, provide valid file path");

                filepath = Console.ReadLine();

                if (!File.Exists(filepath))
                {
                    Console.WriteLine($"Provided file doesn't exist");

                    filepath = default;
                }
            }

            return filepath;
        }
    }
}