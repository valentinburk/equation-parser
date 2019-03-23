using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EquationsParser.Contracts;

namespace EquationsParser.Logic
{
    internal sealed class WebEquationsHandler : IEquationsHandler
    {
        public IEnumerable<string> GetEquations(CancellationToken cancellationToken = default)
        {
            return Enumerable.Empty<string>();
        }

        public void OutputResult(string equation)
        {
        }
    }
}
