using System.Collections.Generic;
using EquationsParser.Models;

namespace EquationsParser
{
    internal sealed class TermEqualityComparer : IEqualityComparer<Term>
    {
        public bool Equals(Term x, Term y)
        {
            return x.Variables.EqualsInside(y.Variables);
        }

        public int GetHashCode(Term obj)
        {
            return obj.GetHashCode();
        }
    }
}
