using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser.Contracts
{
    public interface IAppRunner
    {
        Task RunAppAsync(CancellationToken cancellationToken = default);
    }
}
