using System.Collections.Generic;
using System.Text;

namespace EquationsParser
{
    public class StringParser
    {
        public static string[] Parse(string origin, char[] delimiters)
        {
            List<string> strings = new List<string>();

            // Remove spaces
            origin = origin.Replace(" ", "");

            int index = -1;
            StringBuilder builder = new StringBuilder();

            // Store sign delimiter
            char nextSign = '+';
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

                foreach (char delimiter in delimiters)
                {
                    if (origin[index] != delimiter) continue;

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
