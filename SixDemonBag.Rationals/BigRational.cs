using System.Numerics;

namespace SixDemonBag.Rationals
{
    public struct BigRational : IComparable, IComparable<BigRational>
    {

        public BigInteger Numerator { get; private set; }
        public BigInteger Denominator { get; private set; }

        public BigRational(BigInteger numerator) :
            this(numerator, BigInteger.One)
        { }

        public BigRational(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Denominator cannot be zero.");
            if (numerator == 0)
            {
                denominator = BigInteger.One; // Normalize 0 to 0/1
            }
            else if (denominator < 0)
            {
                numerator = -numerator; // Move the sign to the numerator
                denominator = -denominator;
            }

            var gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        public override readonly string ToString() =>
            Denominator == 1 ? Numerator.ToString() : $"{Numerator}/{Denominator}";

        public readonly int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if (obj is not BigRational)
                throw new ArgumentException("Object is not a BigRational");

            return CompareTo((BigRational)obj);
        }

        public readonly int CompareTo(BigRational other)
        {
            var lhs = Numerator * other.Denominator;
            var rhs = other.Numerator * Denominator;
            return lhs.CompareTo(rhs);
        }

        //Arithmetic operators
        public static BigRational operator +(BigRational a, BigRational b) =>
            new(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static BigRational operator -(BigRational a, BigRational b) =>
            new(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static BigRational operator *(BigRational a, BigRational b) =>
            new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static BigRational operator /(BigRational a, BigRational b)
        {
            if (b.Numerator == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        // Implementing equality
        public override readonly bool Equals(object? obj) =>
            obj is BigRational other && Numerator == other.Numerator && Denominator == other.Denominator;

        public override int GetHashCode() => (Numerator, Denominator).GetHashCode();

        // Equality operators
        public static bool operator ==(BigRational a, BigRational b) => a.Equals(b);
        public static bool operator !=(BigRational a, BigRational b) => !(a == b);

        // Comparison operators
        public static bool operator <(BigRational a, BigRational b) => a.CompareTo(b) < 0;
        public static bool operator >(BigRational a, BigRational b) => a.CompareTo(b) > 0;
        public static bool operator <=(BigRational a, BigRational b) => a.CompareTo(b) <= 0;
        public static bool operator >=(BigRational a, BigRational b) => a.CompareTo(b) >= 0;
    }

}