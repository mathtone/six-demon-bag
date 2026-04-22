using System.Numerics;

namespace Six.Demon.Bag.Rationals;

public readonly struct Rational<T> : IRational<T>
	where T : IBinaryInteger<T> {
	public T Numerator { get; }
	public T Denominator { get; }

	public Rational(T numerator, T denominator) {
		if(denominator == T.Zero)
			throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero.");

		if(numerator == T.Zero) {
			Numerator = T.Zero;
			Denominator = T.One;
			return;
		}

		if(denominator < T.Zero) {
			numerator = checked(-numerator);
			denominator = checked(-denominator);
		}

		var gcd = GetGcd(numerator, denominator);
		Numerator = numerator / gcd;
		Denominator = denominator / gcd;
	}

	private static T GetGcd(T a, T b) {
		a = T.Abs(a);
		b = T.Abs(b);

		while(b != T.Zero)
			(a, b) = (b, a % b);

		return a;
	}

	public readonly int CompareTo(object? obj) =>
		obj is null ? 1 :
		obj is IRational<T> other ? CompareTo(other) :
		throw new ArgumentException("Object must be an IRational<T>.", nameof(obj));

	public readonly int CompareTo(IRational<T>? other) {
		if(other is null)
			return 1;

		var lhs = checked(Numerator * other.Denominator);
		var rhs = checked(other.Numerator * Denominator);
		return lhs.CompareTo(rhs);
	}

	public readonly bool Equals(IRational<T>? other) =>
		other is not null &&
		Numerator == other.Numerator &&
		Denominator == other.Denominator;

	public override readonly bool Equals(object? obj) =>
		obj is IRational<T> other && Equals(other);

	public override readonly int GetHashCode() =>
		HashCode.Combine(Numerator, Denominator);

	public override string ToString() =>
		Denominator == T.One ? Numerator.ToString()! : $"{Numerator}/{Denominator}";

	public static bool operator ==(Rational<T> a, IRational<T> b) => a.Equals(b);
	public static bool operator !=(Rational<T> a, IRational<T> b) => !a.Equals(b);
	public static bool operator <(Rational<T> a, IRational<T> b) => a.CompareTo(b) < 0;
	public static bool operator >(Rational<T> a, IRational<T> b) => a.CompareTo(b) > 0;
	public static bool operator <=(Rational<T> a, IRational<T> b) => a.CompareTo(b) <= 0;
	public static bool operator >=(Rational<T> a, IRational<T> b) => a.CompareTo(b) >= 0;

	public static Rational<T> operator +(Rational<T> a, IRational<T> b) =>
		new(
			checked(a.Numerator * b.Denominator + b.Numerator * a.Denominator),
			checked(a.Denominator * b.Denominator)
		);

	public static Rational<T> operator -(Rational<T> a, IRational<T> b) =>
		new(
			checked(a.Numerator * b.Denominator - b.Numerator * a.Denominator),
			checked(a.Denominator * b.Denominator)
		);

	public static Rational<T> operator *(Rational<T> a, IRational<T> b) =>
		new(
			checked(a.Numerator * b.Numerator),
			checked(a.Denominator * b.Denominator)
		);

	public static Rational<T> operator /(Rational<T> a, IRational<T> b) =>
		b.Numerator == T.Zero ?
			throw new DivideByZeroException("Cannot divide by zero.") :
			new(checked(a.Numerator * b.Denominator), checked(a.Denominator * b.Numerator));
}

public static class Rational {
	public static Rational<byte> R8(byte n, byte d) => new(n, d);
	public static Rational<short> R16(short n, short d) => new(n, d);
	public static Rational<int> R32(int n, int d) => new(n, d);
	public static Rational<long> R64(long n, long d) => new(n, d);
	public static Rational<Int128> R128(Int128 n, Int128 d) => new(n, d);
	public static Rational<BigInteger> RBig(BigInteger n, BigInteger d) => new(n, d);
	public static Rational<T> Create<T>(T n, T d) where T : IBinaryInteger<T> => new(n, d);
}