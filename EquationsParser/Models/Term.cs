using System;

namespace EquationsParser.Models
{
    public struct Term : IEquatable<Term>
    {
        public decimal Multiplier;

        public string[] Variables;

        public static Term operator+ (Term a, Term b)
        {
            Term result;

            result.Multiplier = a.Multiplier + b.Multiplier;
            result.Variables = a.Variables;

            return result;
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
