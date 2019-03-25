using EquationsParser.Models;

namespace EquationsParser.Contracts
{
    internal interface ITermParser
    {
        Term Parse(string term);
    }
}
