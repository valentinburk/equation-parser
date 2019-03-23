using EquationsParser.Contracts;
using EquationsParser.Logic;
using System;
using EquationsParser.Models;

namespace EquationsParser
{
    internal sealed class Program
    {
        private static Config _config;

        private static ICalculator _calculator;
        private static IEquationsHandler _equationsHandler;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (s, a) =>
            {
                a.Cancel = true;
            };

            _config = SetProgramConfig(args);

            _calculator = new Calculator(new StringParser(), new TermParser(), new TermConverter());
            
            StartProgram();
        }

        private static void StartProgram()
        {
            switch (_config.ProgramMode)
            {
                case ProgramMode.Interactive:
                    _equationsHandler = new ConsoleEquationsHandler();
                    break;
                case ProgramMode.FromFile:
                    _equationsHandler = new FileEquationsHandler(_config.InputFilepath, _config.OutputFilepath);
                    break;
            }

            foreach (var equation in _equationsHandler.GetEquations())
            {
                var result = _calculator.Calculate(equation);
                _equationsHandler.OutputResult(result);
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

                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.I:
                        mode = ProgramMode.Interactive;
                        break;
                    case ConsoleKey.F:
                        mode = ProgramMode.FromFile;
                        break;
                }
            }

            return new Config
            {
                ProgramMode = mode,
                InputFilepath = filepath,
            };
        }
    }
}