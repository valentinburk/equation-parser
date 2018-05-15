using System.Collections.Generic;

namespace EquationsParser
{
    public class TermEqualityComparer : IEqualityComparer<Term>
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
