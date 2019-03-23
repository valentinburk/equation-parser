using EnsureThat;
using EquationsParser.Contracts;
using System.Collections.Generic;
using System.IO;

namespace EquationsParser.Logic
{
    internal sealed class FileEquationsHandler : IEquationsHandler
    {
        private readonly string _inputFilepath;
        private readonly string _outputFilepath;

        public FileEquationsHandler(string inputFilepath, string outputFilepath)
        {
            EnsureArg.IsNotNull(inputFilepath, nameof(inputFilepath));
            EnsureArg.IsNotNull(outputFilepath, nameof(outputFilepath));

            _inputFilepath = inputFilepath;
            _outputFilepath = outputFilepath;
        }

        public IEnumerable<string> GetEquations()
        {
            using (var reader = new StreamReader(_inputFilepath))
            {
                var line = reader.ReadLine();

                if (line is null)
                {
                    yield break;
                }

                yield return line;
            }
        }

        public void OutputResult(string equation)
        {
            File.AppendAllLines(_outputFilepath, new[] { equation });
        }
    }
}
