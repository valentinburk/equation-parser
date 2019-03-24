using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EquationsParser.Contracts;

namespace EquationsParser.Logic
{
    internal sealed class WebEquationsHandler : IEquationsHandler
    {
        public IEnumerable<string> GetEquations(CancellationToken cancellationToken = default)
        {
            return Enumerable.Empty<string>();
        }

        public Task OutputResultAsync(string equation)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
