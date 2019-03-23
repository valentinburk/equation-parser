using System;
using System.Collections.Generic;
using System.Threading;
using EquationsParser.Contracts;

namespace EquationsParser.Logic
{
    internal sealed class ConsoleEquationsHandler : IEquationsHandler
    {
        public IEnumerable<string> GetEquations(CancellationToken cancellationToken = default)
        {
            while (true)
            {
                Console.WriteLine("Please, input your expression below or type 'exit' to close the program:");

                var line = Console.ReadLine();

                if (line == "exit" || cancellationToken.IsCancellationRequested)
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
