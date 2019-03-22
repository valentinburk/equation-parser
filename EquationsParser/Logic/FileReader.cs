using EquationsParser.Contracts;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser.Logic
{
    internal sealed class FileReader : IFileReader
    {
        public Task<string[]> ReadEquationsAsync(string filePath, CancellationToken cancellationToken = default)
        {
            return File.ReadAllLinesAsync(filePath, cancellationToken);
        }
    }
}
