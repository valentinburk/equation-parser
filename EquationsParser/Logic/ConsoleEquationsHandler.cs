using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

                if (cancellationToken.IsCancellationRequested || line is null || line == "exit")
                {
                    yield break;
                }

                yield return line;
            }
        }

        public Task OutputResultAsync(string equation)
        {
            Console.WriteLine($"Result: {equation}");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
