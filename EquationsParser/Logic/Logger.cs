using System;
using System.Diagnostics;
using EquationsParser.Contracts;

namespace EquationsParser.Logic
{
    internal sealed class Logger : ILogger
    {
        public void Log(TraceLevel level, string info)
        {
            Console.WriteLine($"{DateTime.UtcNow} [{level}]: {info}");
        }
    }
}
