using System.Numerics;

namespace SixDemonBag;

public readonly struct BigRational :
	IComparable<BigRational>,
	IEquatable<BigRational> {
	private readonly BigInteger denominator;

	public BigRational(BigInteger numerator)
		: this(numerator, BigInteger.One) {
	}

	public BigRational(BigInteger numerator, BigInteger denominator) {
		if(denominator.IsZero)
			throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero.");

		if(denominator.Sign < 0) {
			numerator = -numerator;
			denominator = -denominator;
		}

		var divisor = BigInteger.GreatestCommonDivisor(numerator, denominator);
		Numerator = numerator / divisor;
		this.denominator = denominator / divisor;
	}

	public BigInteger Numerator { get; }
	public BigInteger Denominator => denominator.IsZero ? BigInteger.One : denominator;

	public int CompareTo(BigRational other) =>
		(Numerator * other.Denominator).CompareTo(other.Numerator * Denominator);

	public bool Equals(BigRational other) =>
		Numerator == other.Numerator && Denominator == other.Denominator;

	public override bool Equals(object? obj) => obj is BigRational other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(Numerator, Denominator);

	public override string ToString() =>
		Denominator.IsOne ? Numerator.ToString() : $"{Numerator}/{Denominator}";

	public static BigRational operator +(BigRational left, BigRational right) =>
		new(
			left.Numerator * right.Denominator + right.Numerator * left.Denominator,
			left.Denominator * right.Denominator);

	public static BigRational operator -(BigRational left, BigRational right) =>
		new(
			left.Numerator * right.Denominator - right.Numerator * left.Denominator,
			left.Denominator * right.Denominator);

	public static BigRational operator *(BigRational left, BigRational right) =>
		new(left.Numerator * right.Numerator, left.Denominator * right.Denominator);

	public static BigRational operator /(BigRational left, BigRational right) {
		if(right.Numerator.IsZero)
			throw new DivideByZeroException();

		return new(left.Numerator * right.Denominator, left.Denominator * right.Numerator);
	}

	public static BigRational operator -(BigRational value) => new(-value.Numerator, value.Denominator);

	public static bool operator ==(BigRational left, BigRational right) => left.Equals(right);
	public static bool operator !=(BigRational left, BigRational right) => !left.Equals(right);
	public static bool operator <(BigRational left, BigRational right) => left.CompareTo(right) < 0;
	public static bool operator >(BigRational left, BigRational right) => left.CompareTo(right) > 0;
	public static bool operator <=(BigRational left, BigRational right) => left.CompareTo(right) <= 0;
	public static bool operator >=(BigRational left, BigRational right) => left.CompareTo(right) >= 0;
}

public readonly struct Rational32 :
	IComparable<Rational32>,
	IEquatable<Rational32> {
	private readonly int denominator;

	public Rational32(int numerator, int denominator = 1) {
		if(denominator == 0)
			throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero.");

		var normalized = new BigRational(numerator, denominator);
		Numerator = checked((int)normalized.Numerator);
		this.denominator = checked((int)normalized.Denominator);
	}

	public int Numerator { get; }
	public int Denominator => denominator == 0 ? 1 : denominator;

	public int CompareTo(Rational32 other) =>
		((long)Numerator * other.Denominator).CompareTo((long)other.Numerator * Denominator);

	public bool Equals(Rational32 other) =>
		Numerator == other.Numerator && Denominator == other.Denominator;

	public override bool Equals(object? obj) => obj is Rational32 other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(Numerator, Denominator);

	public override string ToString() =>
		Denominator == 1 ? Numerator.ToString() : $"{Numerator}/{Denominator}";

	public static Rational32 operator +(Rational32 left, Rational32 right) =>
		From(new BigRational(left.Numerator, left.Denominator) + new BigRational(right.Numerator, right.Denominator));

	public static Rational32 operator -(Rational32 left, Rational32 right) =>
		From(new BigRational(left.Numerator, left.Denominator) - new BigRational(right.Numerator, right.Denominator));

	public static Rational32 operator *(Rational32 left, Rational32 right) =>
		From(new BigRational(left.Numerator, left.Denominator) * new BigRational(right.Numerator, right.Denominator));

	public static Rational32 operator /(Rational32 left, Rational32 right) =>
		From(new BigRational(left.Numerator, left.Denominator) / new BigRational(right.Numerator, right.Denominator));

	public static bool operator ==(Rational32 left, Rational32 right) => left.Equals(right);
	public static bool operator !=(Rational32 left, Rational32 right) => !left.Equals(right);
	public static bool operator <(Rational32 left, Rational32 right) => left.CompareTo(right) < 0;
	public static bool operator >(Rational32 left, Rational32 right) => left.CompareTo(right) > 0;
	public static bool operator <=(Rational32 left, Rational32 right) => left.CompareTo(right) <= 0;
	public static bool operator >=(Rational32 left, Rational32 right) => left.CompareTo(right) >= 0;

	private static Rational32 From(BigRational value) =>
		new(checked((int)value.Numerator), checked((int)value.Denominator));
}

public readonly struct Rational64 :
	IComparable<Rational64>,
	IEquatable<Rational64> {
	private readonly long denominator;

	public Rational64(long numerator, long denominator = 1) {
		if(denominator == 0)
			throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero.");

		var normalized = new BigRational(numerator, denominator);
		Numerator = checked((long)normalized.Numerator);
		this.denominator = checked((long)normalized.Denominator);
	}

	public long Numerator { get; }
	public long Denominator => denominator == 0 ? 1 : denominator;

	public int CompareTo(Rational64 other) =>
		((BigInteger)Numerator * other.Denominator).CompareTo((BigInteger)other.Numerator * Denominator);

	public bool Equals(Rational64 other) =>
		Numerator == other.Numerator && Denominator == other.Denominator;

	public override bool Equals(object? obj) => obj is Rational64 other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(Numerator, Denominator);

	public override string ToString() =>
		Denominator == 1 ? Numerator.ToString() : $"{Numerator}/{Denominator}";

	public static Rational64 operator +(Rational64 left, Rational64 right) =>
		From(new BigRational(left.Numerator, left.Denominator) + new BigRational(right.Numerator, right.Denominator));

	public static Rational64 operator -(Rational64 left, Rational64 right) =>
		From(new BigRational(left.Numerator, left.Denominator) - new BigRational(right.Numerator, right.Denominator));

	public static Rational64 operator *(Rational64 left, Rational64 right) =>
		From(new BigRational(left.Numerator, left.Denominator) * new BigRational(right.Numerator, right.Denominator));

	public static Rational64 operator /(Rational64 left, Rational64 right) =>
		From(new BigRational(left.Numerator, left.Denominator) / new BigRational(right.Numerator, right.Denominator));

	public static bool operator ==(Rational64 left, Rational64 right) => left.Equals(right);
	public static bool operator !=(Rational64 left, Rational64 right) => !left.Equals(right);
	public static bool operator <(Rational64 left, Rational64 right) => left.CompareTo(right) < 0;
	public static bool operator >(Rational64 left, Rational64 right) => left.CompareTo(right) > 0;
	public static bool operator <=(Rational64 left, Rational64 right) => left.CompareTo(right) <= 0;
	public static bool operator >=(Rational64 left, Rational64 right) => left.CompareTo(right) >= 0;

	private static Rational64 From(BigRational value) =>
		new(checked((long)value.Numerator), checked((long)value.Denominator));
}