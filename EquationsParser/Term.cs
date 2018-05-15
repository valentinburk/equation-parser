using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EquationsParser
{
    public class Term
    {
        public string[] Variables { get; set; }

        public float Multiplier  { get; set; }

        public static Term FromString(string term)
        {
            Term result = new Term();

            // Match and assign all variables
            Regex regex = new Regex(@"[a-z](\^{1}-?[0-9]*)?");
            result.Variables = regex.Matches(term).Select(o => o.Value).ToArray();
            
            // Match multipliers
            regex = new Regex(@"([^^]-?[0-9]+(\.[0-9]+)?)+");
            var multipliers = regex.Matches(term)
                .Select(o => Convert.ToSingle(o.Value))
                .ToArray();

            // Change sign if needed
            int sign = term[0] == '-' ? -1 : 1; 

            // Assign multiplier
            result.Multiplier = multipliers.Any() ? multipliers.Aggregate((a, b) => a * b) : 1 * sign;

            return result;
        }

        public string ToCanonical()
        {
            // If multiplier equals 0, return empty
            if (Math.Abs(Multiplier) < 0.001f) return String.Empty;

            StringBuilder builder = new StringBuilder();

            if (Multiplier > 0)
            {
                builder.Append('+');
            }

            builder.Append(Multiplier);
            builder.Append(string.Join("", Variables));

            return builder.ToString();
        }

        public override string ToString()
        {
            return $"Multiplier: {Multiplier}; Variables: [{string.Join(", ", Variables)}]";
        }
    }

    public enum TermType
    {
        None = -1,
        Multiplier,
        Variable,
        Power
    }
}
