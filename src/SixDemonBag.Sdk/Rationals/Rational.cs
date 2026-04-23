using System.Numerics;

namespace Six.Demon.Bag.Rationals;

public readonly partial struct Rational<T> : IRational<T>
	where T : IBinaryInteger<T> {

	public T Numerator { get; }
	public T Denominator { get; }
	public bool IsProper => T.Abs(Numerator) < Denominator;
	public bool IsOne => Numerator == Denominator;
	public double Ratio => ToDouble();
	public Rational<T> Negate() => -this;

	public Rational(T numerator, T denominator) {
		if(denominator == T.Zero)
			throw new ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero.");

		if(numerator == T.Zero) {
			Numerator = T.Zero;
			Denominator = T.One;
			return;
		}

		var gcd = GreatestCommonDivisorSigned(numerator, denominator);
		numerator /= gcd;
		denominator /= gcd;

		if(denominator < T.Zero) {
			try {
				numerator = checked(-numerator);
				denominator = checked(-denominator);
			}
			catch(OverflowException ex) {
				throw new OverflowException(
					"Cannot normalize this rational because the denominator is the minimum representable value for the type after reduction.",
					ex);
			}
		}

		Numerator = numerator;
		Denominator = denominator;
	}

	public double ToDouble() =>
		double.CreateChecked(Numerator) / double.CreateChecked(Denominator);

	public bool TryToDouble(out double value) {
		try {
			value = double.CreateChecked(Numerator) / double.CreateChecked(Denominator);
			return true;
		}
		catch(OverflowException) {
			value = default;
			return false;
		}
		catch(ArgumentOutOfRangeException) {
			value = default;
			return false;
		}
	}

	public Rational<T> Reciprocal() => Numerator == T.Zero ?
		throw new DivideByZeroException("Zero does not have a reciprocal.") :
		new Rational<T>(Denominator, Numerator);

	public Rational<T> Abs() => Numerator >= T.Zero ?
		this :
		new(checked(-Numerator), Denominator);

	public readonly int CompareTo(object? obj) =>
		obj is null ? 1 :
		obj is IRational<T> other ? CompareTo(other) :
		throw new ArgumentException($"Object must be an {nameof(IRational<T>)}.", nameof(obj));

	public readonly int CompareTo(IRational<T>? other) {
		if(other is null)
			return 1;

		if(Numerator == other.Numerator && Denominator == other.Denominator)
			return 0;

		var signCompare = T.Sign(Numerator).CompareTo(T.Sign(other.Numerator));
		if(signCompare != 0)
			return signCompare;

		return CompareFractions(Numerator, Denominator, other.Numerator, other.Denominator);
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
}

public readonly partial struct Rational<T> {
	public static bool TryFromDouble(double value, out Rational<T> result) {
		try {
			result = FromDouble(value);
			return true;
		}
		catch(OverflowException) {
			result = default;
			return false;
		}
		catch(ArgumentOutOfRangeException) {
			result = default;
			return false;
		}
	}
	public static Rational<T> FromDouble(double value) {
		if(double.IsNaN(value) || double.IsInfinity(value))
			throw new ArgumentOutOfRangeException(nameof(value), "Value must be a finite number.");

		if(value == 0.0)
			return new Rational<T>(T.Zero, T.One);

		var bits = BitConverter.DoubleToInt64Bits(value);
		var negative = (bits >> 63) != 0;
		var exponentBits = (int)((bits >> 52) & 0x7FF);
		var mantissaBits = bits & 0x000F_FFFF_FFFF_FFFFL;

		ulong significand;
		int exponent;

		if(exponentBits == 0) {
			// Subnormal: no implicit leading 1
			significand = (ulong)mantissaBits;
			exponent = -1074;
		}
		else {
			// Normalized: implicit leading 1
			significand = (1UL << 52) | (ulong)mantissaBits;
			exponent = exponentBits - 1075;
		}

		// Cancel factors of 2 before converting to T.
		if(exponent < 0) {
			while(exponent < 0 && (significand & 1UL) == 0) {
				significand >>= 1;
				exponent++;
			}
		}

		var numerator = T.CreateChecked(significand);
		if(negative)
			numerator = checked(-numerator);

		if(exponent >= 0) {
			numerator <<= exponent;
			return new Rational<T>(numerator, T.One);
		}
		else {
			var denominator = T.One << (-exponent);
			return new Rational<T>(numerator, denominator);
		}
	}

	public static bool TryCreate(T numerator, T denominator, out Rational<T> result) {
		result = default;

		if(denominator == T.Zero) {
			return false;
		}

		try {
			result = new(numerator, denominator);
			return true;
		}
		catch(OverflowException) {
			return false;
		}
		catch(ArgumentOutOfRangeException) {
			return false;
		}
	}


	public static implicit operator Rational<T>(T value) => new(value, T.One);

	public static Rational<T> operator +(Rational<T> value) => value;

	public static Rational<T> operator -(Rational<T> value) =>
		value.Numerator == T.Zero
			? value
			: new(checked(-value.Numerator), value.Denominator);

	public static Rational<T> operator ++(Rational<T> value) =>
		value + T.One;

	public static Rational<T> operator --(Rational<T> value) =>
		value - T.One;

	public static Rational<T> operator +(Rational<T> a, Rational<T> b) => Add(a, b);
	public static Rational<T> operator -(Rational<T> a, Rational<T> b) => Subtract(a, b);
	public static Rational<T> operator *(Rational<T> a, Rational<T> b) => Multiply(a, b);
	public static Rational<T> operator /(Rational<T> a, Rational<T> b) => Divide(a, b);

	public static Rational<T> operator +(Rational<T> a, IRational<T> b) => Add(a, b);
	public static Rational<T> operator -(Rational<T> a, IRational<T> b) => Subtract(a, b);
	public static Rational<T> operator *(Rational<T> a, IRational<T> b) => Multiply(a, b);
	public static Rational<T> operator /(Rational<T> a, IRational<T> b) => Divide(a, b);

	public static Rational<T> operator +(Rational<T> a, T b) => Add(a, new Rational<T>(b, T.One));
	public static Rational<T> operator -(Rational<T> a, T b) => Subtract(a, new Rational<T>(b, T.One));
	public static Rational<T> operator *(Rational<T> a, T b) => Multiply(a, new Rational<T>(b, T.One));
	public static Rational<T> operator /(Rational<T> a, T b) => Divide(a, new Rational<T>(b, T.One));

	public static Rational<T> operator +(T a, Rational<T> b) => Add(new Rational<T>(a, T.One), b);
	public static Rational<T> operator -(T a, Rational<T> b) => Subtract(new Rational<T>(a, T.One), b);
	public static Rational<T> operator *(T a, Rational<T> b) => Multiply(new Rational<T>(a, T.One), b);
	public static Rational<T> operator /(T a, Rational<T> b) => Divide(new Rational<T>(a, T.One), b);

	public static bool operator ==(Rational<T> a, Rational<T> b) => a.Equals(b);
	public static bool operator !=(Rational<T> a, Rational<T> b) => !a.Equals(b);
	public static bool operator <(Rational<T> a, Rational<T> b) => a.CompareTo(b) < 0;
	public static bool operator >(Rational<T> a, Rational<T> b) => a.CompareTo(b) > 0;
	public static bool operator <=(Rational<T> a, Rational<T> b) => a.CompareTo(b) <= 0;
	public static bool operator >=(Rational<T> a, Rational<T> b) => a.CompareTo(b) >= 0;

	public static bool operator ==(Rational<T> a, IRational<T> b) => a.Equals(b);
	public static bool operator !=(Rational<T> a, IRational<T> b) => !a.Equals(b);
	public static bool operator <(Rational<T> a, IRational<T> b) => a.CompareTo(b) < 0;
	public static bool operator >(Rational<T> a, IRational<T> b) => a.CompareTo(b) > 0;
	public static bool operator <=(Rational<T> a, IRational<T> b) => a.CompareTo(b) <= 0;
	public static bool operator >=(Rational<T> a, IRational<T> b) => a.CompareTo(b) >= 0;

	public static bool operator ==(Rational<T> a, T b) => a.Equals(new Rational<T>(b, T.One));
	public static bool operator !=(Rational<T> a, T b) => !a.Equals(new Rational<T>(b, T.One));
	public static bool operator <(Rational<T> a, T b) => a.CompareTo(new Rational<T>(b, T.One)) < 0;
	public static bool operator >(Rational<T> a, T b) => a.CompareTo(new Rational<T>(b, T.One)) > 0;
	public static bool operator <=(Rational<T> a, T b) => a.CompareTo(new Rational<T>(b, T.One)) <= 0;
	public static bool operator >=(Rational<T> a, T b) => a.CompareTo(new Rational<T>(b, T.One)) >= 0;

	public static bool operator ==(T a, Rational<T> b) => new Rational<T>(a, T.One).Equals(b);
	public static bool operator !=(T a, Rational<T> b) => !new Rational<T>(a, T.One).Equals(b);
	public static bool operator <(T a, Rational<T> b) => new Rational<T>(a, T.One).CompareTo(b) < 0;
	public static bool operator >(T a, Rational<T> b) => new Rational<T>(a, T.One).CompareTo(b) > 0;
	public static bool operator <=(T a, Rational<T> b) => new Rational<T>(a, T.One).CompareTo(b) <= 0;
	public static bool operator >=(T a, Rational<T> b) => new Rational<T>(a, T.One).CompareTo(b) >= 0;

	private static Rational<T> Add(in Rational<T> a, IRational<T> b) {
		var gcd = GreatestCommonDivisorPositive(a.Denominator, b.Denominator);

		var leftDen = a.Denominator / gcd;
		var rightDen = b.Denominator / gcd;
		var numerator = checked(a.Numerator * rightDen + b.Numerator * leftDen);
		var denominator = checked(leftDen * b.Denominator);

		return new Rational<T>(numerator, denominator);
	}

	private static Rational<T> Subtract(in Rational<T> a, IRational<T> b) {
		var gcd = GreatestCommonDivisorPositive(a.Denominator, b.Denominator);
		var leftDen = a.Denominator / gcd;
		var rightDen = b.Denominator / gcd;
		var numerator = checked(a.Numerator * rightDen - b.Numerator * leftDen);
		var denominator = checked(leftDen * b.Denominator);

		return new Rational<T>(numerator, denominator);
	}

	private static Rational<T> Multiply(in Rational<T> a, IRational<T> b) {
		var aNum = a.Numerator;
		var aDen = a.Denominator;
		var bNum = b.Numerator;
		var bDen = b.Denominator;

		var gcd1 = GreatestCommonDivisorPositive(aNum, bDen);
		aNum /= gcd1;
		bDen /= gcd1;

		var gcd2 = GreatestCommonDivisorPositive(bNum, aDen);
		bNum /= gcd2;
		aDen /= gcd2;

		return new Rational<T>(
			checked(aNum * bNum),
			checked(aDen * bDen));
	}

	private static Rational<T> Divide(in Rational<T> a, IRational<T> b) {
		if(b.Numerator == T.Zero)
			throw new DivideByZeroException("Cannot divide by zero.");

		var aNum = a.Numerator;
		var aDen = a.Denominator;
		var bNum = b.Numerator;
		var bDen = b.Denominator;

		var gcd1 = GreatestCommonDivisorPositive(aNum, bNum);
		aNum /= gcd1;
		bNum /= gcd1;

		var gcd2 = GreatestCommonDivisorPositive(bDen, aDen);
		bDen /= gcd2;
		aDen /= gcd2;

		return new(
			checked(aNum * bDen),
			checked(aDen * bNum)
		);
	}

	private static int CompareFractions(T leftNumerator, T leftDenominator, T rightNumerator, T rightDenominator) {
		var invertResult = false;

		while(true) {
			var (leftQuotient, leftRemainder) = FloorDivRem(leftNumerator, leftDenominator);
			var (rightQuotient, rightRemainder) = FloorDivRem(rightNumerator, rightDenominator);

			var quotientCompare = leftQuotient.CompareTo(rightQuotient);
			if(quotientCompare != 0)
				return invertResult ? -quotientCompare : quotientCompare;

			var leftIsExact = leftRemainder == T.Zero;
			var rightIsExact = rightRemainder == T.Zero;

			if(leftIsExact || rightIsExact) {
				var result = leftIsExact
					? (rightIsExact ? 0 : -1)
					: 1;

				return invertResult ? -result : result;
			}

			leftNumerator = leftDenominator;
			leftDenominator = leftRemainder;

			rightNumerator = rightDenominator;
			rightDenominator = rightRemainder;

			invertResult = !invertResult;
		}
	}

	private static (T Quotient, T Remainder) FloorDivRem(T numerator, T denominator) {
		var quotient = numerator / denominator;
		var remainder = numerator % denominator;

		if(remainder < T.Zero) {
			quotient -= T.One;
			remainder += denominator;
		}

		return (quotient, remainder);
	}

	private static T GreatestCommonDivisorSigned(T a, T b) {
		while(b != T.Zero)
			(a, b) = (b, a % b);

		return a;
	}

	private static T GreatestCommonDivisorPositive(T a, T b) {
		while(b != T.Zero)
			(a, b) = (b, a % b);

		return a < T.Zero ? checked(-a) : a;
	}
}

public static class Rational {
	public static Rational<byte> R8(byte n, byte d) => new(n, d);
	public static Rational<short> R16(short n, short d) => new(n, d);
	public static Rational<int> R32(int n, int d) => new(n, d);
	public static Rational<long> R64(long n, long d) => new(n, d);
	public static Rational<Int128> R128(Int128 n, Int128 d) => new(n, d);
	public static Rational<BigInteger> RBig(BigInteger n, BigInteger d) => new(n, d);

	public static Rational<T> Create<T>(T n, T d)
		where T : IBinaryInteger<T> =>
		new(n, d);
}