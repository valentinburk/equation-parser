using System.Collections.Generic;
using System.Linq;
using EquationsParser.Contracts;

namespace EquationsParser.Logic
{
    internal sealed class WebEquationsHandler : IEquationsHandler
    {
        public IEnumerable<string> GetEquations()
        {
            return Enumerable.Empty<string>();
        }

        public void OutputResult(string equation)
        {
        }
    }
}
