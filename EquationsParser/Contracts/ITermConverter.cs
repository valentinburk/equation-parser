using EquationsParser.Models;

namespace EquationsParser.Contracts
{
    public interface ITermConverter
    {
        string ToCanonical(Term term);
    }
}
