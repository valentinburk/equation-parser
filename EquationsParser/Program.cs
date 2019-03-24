using EquationsParser.Logic;
using EquationsParser.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser
{
    internal sealed class Program
    {
        static async Task Main(string[] args)
        {
            var cancellationSource = new CancellationTokenSource();

            Console.CancelKeyPress += (s, a) =>
            {
                a.Cancel = true;
                cancellationSource.Cancel();
            };

            var config = SetProgramConfig(args);
            var logger = new Logger();
            var calculator = new Calculator(
                new StringParser(logger),
                new TermParser(new VariableParser(logger), logger),
                new TermConverter(logger),
                logger);

            var appRunner = new AppRunner(
                config,
                calculator,
                cfg =>
                {
                    switch (cfg.ProgramMode)
                    {
                        case ProgramMode.Interactive:
                            return new ConsoleEquationsHandler();
                        case ProgramMode.FromFile:
                            return new FileEquationsHandler(cfg.InputFilepath, cfg.OutputFilepath);
                        default:
                            throw new NotSupportedException($"{cfg.ProgramMode} is not supported");
                    }
                },
                logger);

            await appRunner.RunAppAsync(cancellationSource.Token);
        }

        private static Config SetProgramConfig(string[] args)
        {
            var mode = ProgramMode.NotChosen;

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-I":
                        mode = ProgramMode.Interactive;
                        break;
                    case "-F":
                        mode = ProgramMode.FromFile;
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
                        break;
                }
            }

            return new Config
            {
                ProgramMode = mode,
                InputFilepath = SetInputFilename(args),
            };
        }

        private static string SetInputFilename(string[] args)
        {
            if (args.Length > 1)
            {
                return args[1];
            }

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