using System;
using System.Diagnostics;
using System.Text;
using EquationsParser.Contracts;
using EquationsParser.Models;

namespace EquationsParser.Logic
{
    internal sealed class TermConverter : ITermConverter
    {
        private readonly ILogger _logger;

        public TermConverter(ILogger logger)
        {
            _logger = logger;
        }

        public string ToCanonical(Term term)
        {
            _logger.Log(
                TraceLevel.Info,
                $"Start converting term {term} into the canonical form");

            // If multiplier equals 0, return empty
            if (term.Multiplier == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.Append(term.Multiplier > 0 ? '+' : '-');

            var absMultiplier = Math.Abs(term.Multiplier);
            if (absMultiplier != 1)
            {
                builder.Append(absMultiplier);
            }

            builder.Append(string.Join("", term.Variables));

            _logger.Log(
                TraceLevel.Info,
                $"Term {term} has been converted successfully");

            return builder.ToString();
        }
    }
}
