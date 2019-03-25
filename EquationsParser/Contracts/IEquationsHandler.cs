using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EquationsParser.Contracts
{
    internal interface IEquationsHandler : IDisposable
    {
        IEnumerable<string> GetEquations(CancellationToken cancellationToken = default);

        Task OutputResultAsync(string equation);
    }
}
