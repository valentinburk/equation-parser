using EnsureThat;
using EquationsParser.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser.Logic
{
    internal sealed class FileEquationsHandler : IEquationsHandler
    {
        private readonly FileStream _readerFileStream;
        private readonly FileStream _writerFileStream;
        private readonly StreamReader _streamReader;
        private readonly StreamWriter _streamWriter;

        public FileEquationsHandler(string inputFilepath, string outputFilepath)
        {
            EnsureArg.IsNotNull(inputFilepath, nameof(inputFilepath));
            EnsureArg.IsNotNull(outputFilepath, nameof(outputFilepath));

            _readerFileStream = new FileStream(inputFilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _streamReader = new StreamReader(_readerFileStream);

            _writerFileStream = new FileStream(outputFilepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            _streamWriter = new StreamWriter(_writerFileStream)
            {
                AutoFlush = true,
            };
        }

        public IEnumerable<string> GetEquations(CancellationToken cancellationToken = default)
        {
            string line;
            while ((line = _streamReader.ReadLine()) != default)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                yield return line;
            }
        }

        public Task OutputResultAsync(string equation)
        {
            return _streamWriter.WriteLineAsync(equation);
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
            _readerFileStream?.Dispose();
            _streamWriter?.Dispose();
            _writerFileStream?.Dispose();
        }
    }
}
