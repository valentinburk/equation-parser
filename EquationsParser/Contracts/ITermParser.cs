using EquationsParser.Models;

namespace EquationsParser.Contracts
{
    public interface ITermParser
    {
        Term Parse(string term);
    }
}
