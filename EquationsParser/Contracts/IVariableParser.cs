using EquationsParser.Models;

namespace EquationsParser.Contracts
{
    public interface IVariableParser
    {
        Variable Parse(string variable);
    }
}
