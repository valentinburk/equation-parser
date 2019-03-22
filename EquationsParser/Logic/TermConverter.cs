using System.Text;
using EquationsParser.Contracts;
using EquationsParser.Models;

namespace EquationsParser.Logic
{
    internal sealed class TermConverter : ITermConverter
    {
        public string ToCanonical(Term term)
        {
            // If multiplier equals 0, return empty
            if (term.Multiplier == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            if (term.Multiplier > 0)
            {
                builder.Append('+');
            }

            builder.Append(term.Multiplier);
            builder.Append(string.Join("", term.Variables));

            return builder.ToString();
        }
    }
}
