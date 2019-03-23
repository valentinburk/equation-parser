using System;

namespace EquationsParser.Exceptions
{
    internal sealed class InvalidEquationException : Exception
    {
        public InvalidEquationException()
        {
        }

        public InvalidEquationException(string message)
            : base(message)
        {
        }
    }
}
