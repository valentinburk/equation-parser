using System.Collections.Generic;
using System.Linq;
using EquationsParser.Contracts;
using EquationsParser.Models;

namespace EquationsParser.Logic
{
    internal sealed class Calculator : ICalculator
    {
        private readonly IStringParser _stringParser;
        private readonly ITermParser _termParser;
        private readonly ITermConverter _termConverter;
        private readonly IEqualityComparer<Term> _comparer;

        public Calculator(
            IStringParser stringParser,
            ITermParser termParser,
            ITermConverter termConverter)
        {
            _stringParser = stringParser;
            _termParser = termParser;
            _termConverter = termConverter;
            _comparer = new TermEqualityComparer();
        }

        public string Calculate(string equation)
        {
            var terms = new List<Term>();

            var sides = equation.Split("=");

            var leftSide = _stringParser
                .Parse(sides[0])
                .Select(_termParser.Parse);

            var rightSide = _stringParser
                .Parse(sides[1])
                .Select(_termParser.Parse);

            // Add left side terms to resulting list
            foreach (var left in leftSide)
            {
                AddToResult(terms, left);
            }

            // Add right side terms to resulting list
            foreach (var right in rightSide)
            {
                AddToResult(terms, new Term
                {
                    Multiplier = right.Multiplier * -1,
                    Variables = right.Variables
                });
            }

            return ConvertToCanonical(terms);
        }

        private string ConvertToCanonical(IReadOnlyCollection<Term> terms)
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
    }
}
