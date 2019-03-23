using System.Collections.Generic;
using System.Text;
using EquationsParser.Contracts;

namespace EquationsParser.Logic
{
    internal sealed class StringParser : IStringParser
    {
        private static readonly char[] DefaultDelimiters = { '+', '-' };

        public string[] Parse(string origin, IReadOnlyCollection<char> delimiters = default)
        {
            var strings = new List<string>();

            // Remove spaces
            origin = origin.Replace(" ", "");

            var index = -1;
            var builder = new StringBuilder();

            // Store sign delimiter
            var nextSign = '+';
            if (origin[0] == '-')
            {
                nextSign = '-';
                index++;
            }

            while (++index < origin.Length)
            {
                if (nextSign > char.MinValue)
                {
                    builder.Append(nextSign);
                    nextSign = char.MinValue;
                }

                builder.Append(origin[index]);

                foreach (var delimiter in delimiters ?? DefaultDelimiters)
                {
                    if (origin[index] != delimiter)
                    {
                        continue;
                    }

                    builder.Remove(builder.Length - 1, 1);

                    nextSign = delimiter;

                    strings.Add(builder);
                    builder.Clear();

                    break;
                }
            }

            strings.Add(builder);

            return strings.ToArray();
        }
    }
}
