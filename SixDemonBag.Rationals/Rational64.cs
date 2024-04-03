namespace SixDemonBag.Rationals {

	public struct Rational64 : IComparable<Rational64>, IEquatable<Rational64> {

		public long Numerator { get; private set; }
		public long Denominator { get; private set; }

		public Rational64(long numerator, long denominator = 1) {
			if (denominator == 0) throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
			if (numerator == 0) {
				denominator = 1;
			}
			else if (denominator < 0) {
				numerator = -numerator;
				denominator = -denominator;
			}

			long gcd = GCD(numerator, denominator);
			Numerator = numerator / gcd;
			Denominator = denominator / gcd;
		}

		private static long GCD(long a, long b) {
			while (b != 0) {
				long temp = b;
				b = a % b;
				a = temp;
			}
			return Math.Abs(a);
		}

		public static Rational64 operator +(Rational64 a, Rational64 b) =>
			new(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);

		public static Rational64 operator -(Rational64 a, Rational64 b) =>
			new(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);

		public static Rational64 operator *(Rational64 a, Rational64 b) =>
			new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

		public static Rational64 operator /(Rational64 a, Rational64 b) {
			if (b.Numerator == 0)
				throw new DivideByZeroException("Division by zero.");

			return new(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
		}

		public readonly int CompareTo(Rational64 other) {
			long lhs = Numerator * other.Denominator;
			long rhs = other.Numerator * Denominator;
			return lhs.CompareTo(rhs);
		}

		public readonly bool Equals(Rational64 other) =>
			Numerator == other.Numerator && Denominator == other.Denominator;

		public override readonly bool Equals(object? obj) =>
			obj is Rational64 other && Equals(other);

		public override readonly int GetHashCode() =>
			HashCode.Combine(Numerator, Denominator);

		public static bool operator ==(Rational64 left, Rational64 right) => left.Equals(right);
		public static bool operator !=(Rational64 left, Rational64 right) => !(left == right);
		public static bool operator <(Rational64 left, Rational64 right) => left.CompareTo(right) < 0;
		public static bool operator >(Rational64 left, Rational64 right) => left.CompareTo(right) > 0;
		public static bool operator <=(Rational64 left, Rational64 right) => left.CompareTo(right) <= 0;
		public static bool operator >=(Rational64 left, Rational64 right) => left.CompareTo(right) >= 0;

		public override readonly string ToString() =>
			Denominator == 1 ? $"{Numerator}" : $"{Numerator}/{Denominator}";
	}
}