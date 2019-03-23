using System;
using System.Collections.Generic;
using EquationsParser.Contracts;

namespace EquationsParser.Logic
{
    internal sealed class ConsoleEquationsHandler : IEquationsHandler
    {
        public IEnumerable<string> GetEquations()
        {
            while (true)
            {
                Console.WriteLine("Please, input your expression belowm or type 'exit' to close the program:");

                var line = Console.ReadLine();

                if (line == "exit")
                {
                    yield break;
                }

                yield return line;
            }
        }

        public void OutputResult(string equation)
        {
            Console.WriteLine($"Result: {equation}");
        }
    }
}
