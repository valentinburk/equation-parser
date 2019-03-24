using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser.Contracts
{
    public interface IEquationsHandler : IDisposable
    {
        IEnumerable<string> GetEquations(CancellationToken cancellationToken = default);

        Task OutputResultAsync(string equation);
    }
}
