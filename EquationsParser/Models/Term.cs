using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationsParser.Models
{
    public struct Term : IEquatable<Term>
    {
        public decimal Multiplier;

        public Variable[] Variables;

        public Term(IReadOnlyCollection<Variable> variables)
            : this(1m, variables)
        {
        }

        public Term(decimal multiplier, IReadOnlyCollection<Variable> variables)
        {
            Multiplier = multiplier;
            Variables = variables.ToArray();
        }

        public static Term operator + (Term a, Term b)
        {
            Term result;

            result.Multiplier = a.Multiplier + b.Multiplier;
            result.Variables = a.Variables;

            return result;
        }

        public static Term operator * (Term term, int multiplier)
        {
            return new Term(
                term.Multiplier * multiplier,
                term.Variables);
        }

        public bool Equals(Term other)
        {
            return Multiplier == other.Multiplier &&
                   Variables.EqualsInside(other.Variables);
        }

        public override string ToString()
        {
            return $"{Multiplier}{string.Concat(Variables)}";
        }
    }
}
