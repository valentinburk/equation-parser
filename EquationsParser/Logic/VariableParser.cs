using System;
using System.Diagnostics;
using EnsureThat;
using EquationsParser.Contracts;
using EquationsParser.Models;

namespace EquationsParser.Logic
{
    internal sealed class VariableParser : IVariableParser
    {
        private readonly ILogger _logger;

        public VariableParser(ILogger logger)
        {
            _logger = logger;
        }

        public Variable Parse(string variable)
        {
            EnsureArg.IsNotEmptyOrWhitespace(variable, nameof(variable));

            _logger.Log(
                TraceLevel.Info,
                $"Start parsing variable {variable}");

            var parts = variable.Split('^');

            var letter = Convert.ToChar(parts[0]);
            
            if (parts.Length > 1)
            {
                var power = Convert.ToDecimal(parts[1]);

                return new Variable(letter, power);
            }

            return new Variable(letter);
        }
    }
}
