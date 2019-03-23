using System.Collections.Generic;

namespace EquationsParser.Contracts
{
    public interface IEquationsHandler
    {
        IEnumerable<string> GetEquations();

        void OutputResult(string equation);
    }
}
