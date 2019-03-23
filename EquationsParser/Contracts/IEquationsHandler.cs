using System.Collections.Generic;
using System.Threading;

namespace EquationsParser.Contracts
{
    public interface IEquationsHandler
    {
        IEnumerable<string> GetEquations(CancellationToken cancellationToken = default);

        void OutputResult(string equation);
    }
}
