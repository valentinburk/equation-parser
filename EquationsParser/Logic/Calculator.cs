using EquationsParser.Contracts;
using EquationsParser.Exceptions;
using EquationsParser.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EquationsParser.Logic
{
    internal sealed class Calculator : ICalculator
    {
        private readonly IStringParser _stringParser;
        private readonly ITermParser _termParser;
        private readonly ITermConverter _termConverter;
        private readonly ILogger _logger;
        private readonly IEqualityComparer<Term> _comparer;

        public Calculator(
            IStringParser stringParser,
            ITermParser termParser,
            ITermConverter termConverter,
            ILogger logger)
        {
            _stringParser = stringParser;
            _termParser = termParser;
            _termConverter = termConverter;
            _logger = logger;
            _comparer = new TermEqualityComparer();
        }

        public string Calculate(string equation)
        {
            _logger.Log(
                TraceLevel.Info,
                $"Start processing equation {equation}");

            var sides = ValidateEquation(equation);

            var leftSide = _stringParser
                .Parse(sides[0])
                .Select(_termParser.Parse)
                .ToArray();

            var rightSide = _stringParser
                .Parse(sides[1])
                .Select(_termParser.Parse)
                .ToArray();

            var terms = new List<Term>();

            // Add left side terms to resulting list
            foreach (var left in leftSide)
            {
                AddToResult(terms, left);
            }

            // Add right side terms to resulting list
            foreach (var right in rightSide)
            {
                AddToResult(terms, right * -1);
            }

            var result = ConvertToCanonical(terms);

            _logger.Log(
                TraceLevel.Info,
                $"Equation has been successfully converted");

            return result;
        }

        private string ConvertToCanonical(IEnumerable<Term> terms)
        {
            var canonical = terms
                .Select(_termConverter.ToCanonical)
                .OrderByDescending(o => o.Length)
                .ToArray();

            if (canonical.All(string.IsNullOrWhiteSpace))
            {
                return "0=0";
            }

            canonical[0] = canonical[0].TrimStart('+');

            return $"{string.Join("", canonical)}=0";
        }

        private void AddToResult(ICollection<Term> result, Term term)
        {
            if (result.Contains(term, _comparer))
            {
                var added = result.Single(o => o.Variables.EqualsInside(term.Variables));
                result.Remove(added);
                result.Add(added + term);
            }
            else
            {
                result.Add(term);
            }
        }

        private string[] ValidateEquation(string equation)
        {
            if (string.IsNullOrWhiteSpace(equation))
            {
                throw new InvalidEquationException("Equation is empty");
            }

            if (!equation.Contains('='))
            {
                throw new InvalidEquationException("Equation doesn't have equals sign");
            }

            if (equation.Contains("--") ||
                equation.Contains("++") ||
                equation.Contains("-+") ||
                equation.Contains("+-"))
            {
                throw new InvalidEquationException("Equation contains inconsistent sequence of operator: (\"--\", \"++\", \"-+\" or \"+-\")");
            }

            var sides = equation.Split("=");

            if (sides.Length < 2)
            {
                throw new InvalidEquationException("Equation doesn't have terms on both sides");
            }

            return sides
                .Select(side => side.TrimStart('+'))
                .ToArray();
        }
    }
}
