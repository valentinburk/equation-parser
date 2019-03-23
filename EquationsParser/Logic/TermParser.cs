using System;
using System.Linq;
using System.Text.RegularExpressions;
using EquationsParser.Contracts;
using EquationsParser.Models;

namespace EquationsParser.Logic
{
    internal sealed class TermParser : ITermParser
    {
        public Term Parse(string term)
        {
            Term result;

            // Match and assign all variables
            var regex = new Regex(@"[a-z](\^{1}-?[0-9]*)?");
            result.Variables = regex.Matches(term)
                .Select(o => o.Value)
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
            result.Multiplier = multipliers.Any() ?
                multipliers.Aggregate((a, b) => a * b) :
                1 * sign;

            return result;
        }
    }
}
