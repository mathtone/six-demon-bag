using System.Numerics;

namespace Six.Demon.Bag.Rationals;

public interface IRational<T>
	where T : INumber<T> {
	T Numerator { get; }
	T Denominator { get; }
}

public readonly struct BigRational :
	IComparable,
	IComparable<BigRational>,
	IEquatable<BigRational> {

	public BigInteger Numerator { get; }
	public BigInteger Denominator { get; }

	public bool IsZero => Numerator.IsZero;
	public bool IsInteger => Denominator.IsOne;
	public int Sign => Numerator.Sign;

	public static BigRational Zero => new(BigInteger.Zero);
	public static BigRational One => new(BigInteger.One);

	public BigRational(BigInteger numerator)
		: this(numerator, BigInteger.One) { }

	public BigRational(BigInteger numerator, BigInteger denominator) {
		if(denominator.IsZero)
			throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero.");

		if(numerator.IsZero) {
			Numerator = BigInteger.Zero;
			Denominator = BigInteger.One;
			return;
		}

		if(denominator.Sign < 0) {
			numerator = -numerator;
			denominator = -denominator;
		}

		var gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
		Numerator = numerator / gcd;
		Denominator = denominator / gcd;
	}

	public readonly BigRational Abs() => Sign < 0 ? -this : this;
	public readonly BigRational Inverse() =>
		Numerator.IsZero
			? throw new DivideByZeroException("Zero has no multiplicative inverse.")
			: new(Denominator, Numerator);

	public override readonly string ToString() =>
		Denominator.IsOne ? Numerator.ToString() : $"{Numerator}/{Denominator}";

	public readonly int CompareTo(object? obj) =>
		obj is null ? 1 :
		obj is BigRational other ? CompareTo(other) :
		throw new ArgumentException("Object must be a BigRational.", nameof(obj));

	public readonly int CompareTo(BigRational other) {
		var lhs = Numerator * other.Denominator;
		var rhs = other.Numerator * Denominator;
		return lhs.CompareTo(rhs);
	}

	public readonly bool Equals(BigRational other) =>
		Numerator == other.Numerator && Denominator == other.Denominator;

	public override readonly bool Equals(object? obj) =>
		obj is BigRational other && Equals(other);

	public override readonly int GetHashCode() =>
		HashCode.Combine(Numerator, Denominator);

	public static BigRational operator +(BigRational value) => value;

	public static BigRational operator -(BigRational value) =>
		new(-value.Numerator, value.Denominator);

	public static BigRational operator +(BigRational a, BigRational b) =>
		new(
			a.Numerator * b.Denominator + b.Numerator * a.Denominator,
			a.Denominator * b.Denominator);

	public static BigRational operator -(BigRational a, BigRational b) =>
		new(
			a.Numerator * b.Denominator - b.Numerator * a.Denominator,
			a.Denominator * b.Denominator);

	public static BigRational operator *(BigRational a, BigRational b) {
		var gcd1 = BigInteger.GreatestCommonDivisor(a.Numerator, b.Denominator);
		var gcd2 = BigInteger.GreatestCommonDivisor(b.Numerator, a.Denominator);

		return new BigRational(
			(a.Numerator / gcd1) * (b.Numerator / gcd2),
			(a.Denominator / gcd2) * (b.Denominator / gcd1));
	}

	public static BigRational operator /(BigRational a, BigRational b) {
		if(b.Numerator.IsZero)
			throw new DivideByZeroException("Cannot divide by zero.");

		var gcd1 = BigInteger.GreatestCommonDivisor(a.Numerator, b.Numerator);
		var gcd2 = BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator);

		return new BigRational(
			(a.Numerator / gcd1) * (b.Denominator / gcd2),
			(a.Denominator / gcd2) * (b.Numerator / gcd1));
	}

	public static bool operator ==(BigRational a, BigRational b) => a.Equals(b);
	public static bool operator !=(BigRational a, BigRational b) => !a.Equals(b);
	public static bool operator <(BigRational a, BigRational b) => a.CompareTo(b) < 0;
	public static bool operator >(BigRational a, BigRational b) => a.CompareTo(b) > 0;
	public static bool operator <=(BigRational a, BigRational b) => a.CompareTo(b) <= 0;
	public static bool operator >=(BigRational a, BigRational b) => a.CompareTo(b) >= 0;

	public static implicit operator BigRational(int value) => new(value);
	public static implicit operator BigRational(long value) => new(value);
	public static implicit operator BigRational(BigInteger value) => new(value);

	public static explicit operator double(BigRational value) =>
		(double)value.Numerator / (double)value.Denominator;

	public static explicit operator decimal(BigRational value) =>
		(decimal)value.Numerator / (decimal)value.Denominator;

	public readonly void Deconstruct(out BigInteger numerator, out BigInteger denominator) {
		numerator = Numerator;
		denominator = Denominator;
	}
}