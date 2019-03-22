using EquationsParser.Contracts;
using EquationsParser.Logic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EquationsParser
{
    class Program
    {
        private static IFileReader _fileReader;
        private static IStringParser _stringParser;
        private static ITermParser _termParser;
        private static ITermConverter _termConverter;
        private static ICalculator _calculator;

        static Task Main(string[] args)
        {
            _fileReader = new FileReader();
            _stringParser = new StringParser();
            _termParser = new TermParser();
            _termConverter = new TermConverter();
            _calculator = new Calculator(_stringParser, _termParser, _termConverter);

            var type = ProgramMode.NotChosen;
            string filepath = null;

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-I":
                        type = ProgramMode.Interactive;
                        break;
                    case "-F":
                        type = ProgramMode.FromFile;
                        if (args.Length > 1)
                        {
                            filepath = args[1];
                        }
                        break;
                }
            }

            return StartProgram(type, filepath);
        }

        private static async Task StartProgram(ProgramMode type, string filepath)
        {
            while (type == ProgramMode.NotChosen)
            {
                Console.WriteLine("Please, choose program mode:");
                Console.WriteLine("I - interactive");
                Console.WriteLine("F - file");

                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.I:
                        type = ProgramMode.Interactive;
                        break;
                    case ConsoleKey.F:
                        type = ProgramMode.FromFile;
                        break;
                }
            }

            switch (type)
            {
                case ProgramMode.Interactive:
                    RunInteractiveMode();
                    break;
                case ProgramMode.FromFile:
                    await RunFileModeAsync(filepath);
                    break;
            }
        }

        private static void RunInteractiveMode()
        {
            var isRunning = true;

            Console.CancelKeyPress += (s, a) =>
            {
                Console.WriteLine("as");
                isRunning = false;
                a.Cancel = true;
            };

            while (isRunning)
            {
                Console.WriteLine("Please, input your expression below:");
                var expression = Console.ReadLine();

                try
                {
                    var result = _calculator.Calculate(expression);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Parsing failed. Expression is not valid: {expression}");
                }
            }
        }

        private static async Task RunFileModeAsync(string filepath)
        {
            while (!Path.IsPathRooted(filepath))
            {
                Console.WriteLine("Please, provide valid path to your file (full path C:\\SomeDir\\SomeFile.txt):");
                filepath = Console.ReadLine();
            }

            var equations = await _fileReader.ReadEquationsAsync(filepath);
            var calculations = equations.Select(_calculator.Calculate);

            await File.AppendAllLinesAsync($"{DateTime.Now:ddMMyyyyHHmmss}.out", calculations);
        }
    }
}