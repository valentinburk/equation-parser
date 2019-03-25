using EquationsParser.Models;

namespace EquationsParser.Contracts
{
    internal interface ITermConverter
    {
        string ToCanonical(Term term);
    }
}
