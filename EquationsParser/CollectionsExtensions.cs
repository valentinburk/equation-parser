using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquationsParser
{
    public static class CollectionsExtensions
    {
        public static void Add(this IList<string> list, StringBuilder builder)
        {
            list.Add(builder.ToString().Trim());
        }

        public static bool EqualsInside(this IEnumerable<string> origin, IEnumerable<string> other)
        {
            if (origin == null || other == null)
            {
                return false;
            }

            // Enumerate once
            var originArray = origin.ToArray();
            var otherArray = other.ToArray();

            if (!originArray.Any() || !otherArray.Any())
            {
                return false;
            }

            if (originArray.Length != otherArray.Length)
            {
                return false;
            }

            return originArray.All(o => otherArray.Contains(o, StringComparer.InvariantCultureIgnoreCase));
        }
    }
}
