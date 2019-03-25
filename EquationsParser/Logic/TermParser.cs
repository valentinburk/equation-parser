using EquationsParser.Contracts;
using EquationsParser.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using EquationsParser.Exceptions;

namespace EquationsParser.Logic
{
    internal sealed class TermParser : ITermParser
    {
        private readonly IVariableParser _variableParser;
        private readonly ILogger _logger;

        public TermParser(IVariableParser variableParser, ILogger logger)
        {
            _variableParser = variableParser;
            _logger = logger;
        }

        public Term Parse(string term)
        {
            ValidateTerm(term);

            _logger.Log(
                TraceLevel.Info,
                $"Start parsing term {term}");

            // Match and assign all variables
            var regex = new Regex(@"[a-z](\^{1}[-+]?[0-9]*(-?\.[0-9]+)?)?");
            var variables = regex.Matches(term)
                .Select(o => _variableParser.Parse(o.Value))
                .ToArray();

            // Match multipliers
            regex = new Regex(@"^([-+]?[0-9]+(\.[0-9]+)?)+");
            var multipliers = regex.Matches(term)
                .Select(o => Convert.ToDecimal(o.Value))
                .ToArray();

            // Change sign if needed
            var sign = term[0] == '-' ?
                -1 :
                1;

            // Assign multiplier
            var multiplier = multipliers.Any() ?
                multipliers.Aggregate((a, b) => a * b) :
                1 * sign;

            _logger.Log(
                TraceLevel.Info,
                $"Term {term} has been parsed successfully");

            return new Term(multiplier, variables);
        }

        private void ValidateTerm(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                throw new InvalidEquationException("Term is empty");
            }
        }
    }
}
