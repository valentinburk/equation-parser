using System.Collections.Generic;

namespace EquationsParser.Contracts
{
    public interface IStringParser
    {
        string[] Parse(string origin, IReadOnlyCollection<char> delimiters = default);
    }
}
