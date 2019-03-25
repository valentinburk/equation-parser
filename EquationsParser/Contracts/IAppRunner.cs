using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser.Contracts
{
    internal interface IAppRunner
    {
        Task RunAppAsync(CancellationToken cancellationToken = default);
    }
}
