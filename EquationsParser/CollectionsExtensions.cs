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

        public static bool EqualsInside<T>(this IEnumerable<T> origin, IEnumerable<T> other)
            where T : IEquatable<T>
        {
            if (origin == null || other == null)
            {
                return false;
            }

            var originArray = origin.ToArray();
            var otherArray = other.ToArray();

            return originArray.Length == otherArray.Length &&
                   originArray.All(otherArray.Contains);
        }
    }
}
