using System;
using System.Collections.Generic;
using System.IO;

namespace EquationsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramMode type = ProgramMode.NotChosen;
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

            StartProgram(type, filepath);
        }

        private static void StartProgram(ProgramMode type, string filepath)
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
                    RunFileMode(filepath);
                    break;
            }
        }

        private static void RunInteractiveMode()
        {
            Calculator calculator = new Calculator();

            bool isRunning = true;

            Console.CancelKeyPress += (s, a) =>
            {
                Console.WriteLine("as");
                isRunning = false;
                a.Cancel = true;
            };

            while (isRunning)
            {
                Console.WriteLine("Please, input your expression below:");
                string expression = Console.ReadLine();

                try
                {
                    string result = calculator.Calculate(expression);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Parsing failed. Expression is not valid: {expression}");
                }
            }
        }

        private static void RunFileMode(string filepath)
        {
            Calculator calculator = new Calculator();

            while (!Path.IsPathRooted(filepath))
            {
                Console.WriteLine("Please, provide valid path to your file (full path C:\\SomeDir\\SomeFile.txt):");
                filepath = Console.ReadLine();
            }

            List<string> calculations = new List<string>();

            string output = $"{DateTime.Now:ddMMyyyyHHmmss}.out";
            using (var reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        string result = calculator.Calculate(line);
                        calculations.Add(result);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Parsing failed. Expression is not valid: {line}");
                    }
                }
            }

            File.AppendAllLines(output, calculations);
        }
    }
}