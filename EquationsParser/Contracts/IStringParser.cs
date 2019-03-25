using System.Collections.Generic;

namespace EquationsParser.Contracts
{
    internal interface IStringParser
    {
        string[] Parse(string origin, IReadOnlyCollection<char> delimiters = default);
    }
}
