using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser.Contracts
{
    public interface IFileReader
    {
        Task<string[]> ReadEquationsAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
