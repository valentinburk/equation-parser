using System.Diagnostics;

namespace EquationsParser.Contracts
{
    internal interface ILogger
    {
        void Log(TraceLevel level, string info);
    }
}
