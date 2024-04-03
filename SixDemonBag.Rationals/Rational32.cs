using System;

namespace SixDemonBag.Rationals {

    public struct Rational32 : IComparable<Rational32>, IEquatable<Rational32>
    {

        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        public Rational32(int numerator, int denominator = 1)
        {
            if (denominator == 0) throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
            if (numerator == 0)
            {
                denominator = 1;
            }
            else if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            int gcd = GCD(numerator, denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        public static Rational32 operator +(Rational32 a, Rational32 b) =>
            new(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static Rational32 operator -(Rational32 a, Rational32 b) =>
            new(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static Rational32 operator *(Rational32 a, Rational32 b) =>
            new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static Rational32 operator /(Rational32 a, Rational32 b)
        {
            if (b.Numerator == 0)
                throw new DivideByZeroException("Division by zero.");

            return new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public readonly int CompareTo(Rational32 other)
        {
            long lhs = (long)Numerator * other.Denominator;
            long rhs = (long)other.Numerator * Denominator;
            return lhs.CompareTo(rhs);
        }

        public readonly bool Equals(Rational32 other) =>
            Numerator == other.Numerator && Denominator == other.Denominator;

        public override readonly bool Equals(object? obj) =>
            obj is Rational32 other && Equals(other);

        public override readonly int GetHashCode() =>
            HashCode.Combine(Numerator, Denominator);

        public static bool operator ==(Rational32 left, Rational32 right) => left.Equals(right);
        public static bool operator !=(Rational32 left, Rational32 right) => !(left == right);
        public static bool operator <(Rational32 left, Rational32 right) => left.CompareTo(right) < 0;
        public static bool operator >(Rational32 left, Rational32 right) => left.CompareTo(right) > 0;
        public static bool operator <=(Rational32 left, Rational32 right) => left.CompareTo(right) <= 0;
        public static bool operator >=(Rational32 left, Rational32 right) => left.CompareTo(right) >= 0;

        public override readonly string ToString() =>
            Denominator == 1 ? $"{Numerator}" : $"{Numerator}/{Denominator}";
    }
}