using System;

namespace EquationsParser.Models
{
    internal struct Variable : IEquatable<Variable>
    {
        public char Letter;

        public decimal Power;

        public Variable(char letter)
            : this(letter, 1)
        {
        }

        public Variable(char letter, decimal power)
        {
            Letter = letter;
            Power = power;
        }

        public bool Equals(Variable other)
        {
            return Letter.Equals(other.Letter) &&
                   Power.Equals(other.Power);
        }

        public override string ToString()
        {
            return Power != 1 ?
                $"{Letter}^{Power}" :
                Letter.ToString();
        }
    }
}
