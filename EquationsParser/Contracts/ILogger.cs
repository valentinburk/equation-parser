using System.Diagnostics;

namespace EquationsParser.Contracts
{
    public interface ILogger
    {
        void Log(TraceLevel level, string info);
    }
}
