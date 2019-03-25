using EquationsParser.Models;

namespace EquationsParser.Contracts
{
    internal interface IVariableParser
    {
        Variable Parse(string variable);
    }
}
