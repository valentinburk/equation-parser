using System.Collections.Generic;
using System.Linq;

namespace EquationsParser
{
    public class Calculator
    {
        private readonly char[] _delimiters = { '+', '-' };
        private readonly TermEqualityComparer _comparer = new TermEqualityComparer();

        public string Calculate(string str)
        {
            var result = new List<Term>();

            string[] sides = str.Split("=");

            var leftSide = StringParser.Parse(sides[0], _delimiters)
                .Select(Term.FromString);

            var rightSide = StringParser.Parse(sides[1], _delimiters)
                .Select(Term.FromString);

            // Add left side terms to resulting list
            foreach (Term left in leftSide)
            {
                AddToResult(result, left);
            }

            // Add right side terms to resulting
            foreach (Term right in rightSide)
            {
                right.Multiplier = right.Multiplier * -1;
                AddToResult(result, right);
            }

            var canonical = result.Select(o => o.ToCanonical())
                .OrderByDescending(o => o.Length)
                .ToArray();

            if (canonical.All(string.IsNullOrWhiteSpace))
            {
                return "0=0";
            }

            if (canonical[0][0] == '+')
            {
                canonical[0] = canonical[0].Substring(1, canonical[0].Length - 1);
            }

            return $"{string.Join("", canonical)}=0";
        }

        private void AddToResult(List<Term> result, Term term)
        {
            if (result.Contains(term, _comparer))
            {
                var added = result.Single(o => o.Variables.EqualsInside(term.Variables));
                added.Multiplier = added.Multiplier + term.Multiplier;
            }
            else
            {
                result.Add(term);
            }
        }
    }
}
