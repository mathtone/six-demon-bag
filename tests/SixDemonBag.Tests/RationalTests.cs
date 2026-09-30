namespace SixDemonBag.Tests;

public class RationalTests {
	[Fact]
	public void BigRationalNormalizesValueAndSign() {
		var value = new BigRational(6, -8);

		Assert.Equal(-3, value.Numerator);
		Assert.Equal(4, value.Denominator);
		Assert.Equal("-3/4", value.ToString());
	}

	[Fact]
	public void DefaultBigRationalIsZero() =>
		Assert.Equal(new BigRational(0), default(BigRational));

	[Fact]
	public void BigRationalSupportsArithmetic() {
		var left = new BigRational(1, 2);
		var right = new BigRational(1, 3);

		Assert.Equal(new BigRational(5, 6), left + right);
		Assert.Equal(new BigRational(1, 6), left - right);
		Assert.Equal(new BigRational(1, 6), left * right);
		Assert.Equal(new BigRational(3, 2), left / right);
	}

	[Fact]
	public void Rational32NormalizesAndComparesWithoutOverflow() {
		var half = new Rational32(2, 4);

		Assert.Equal("1/2", half.ToString());
		Assert.True(new Rational32(int.MaxValue) > half);
	}

	[Fact]
	public void Rational64ComparesWithoutOverflow() =>
		Assert.True(
			new Rational64(long.MaxValue, long.MaxValue - 1) >
			new Rational64(long.MaxValue - 1, long.MaxValue));

	[Fact]
	public void ZeroDenominatorIsRejected() =>
		Assert.Throws<ArgumentOutOfRangeException>(() => new Rational32(1, 0));

	[Fact]
	public void DivisionByZeroIsRejected() =>
		Assert.Throws<DivideByZeroException>(() => new Rational64(1) / default(Rational64));

	[Fact]
	public void FixedWidthArithmeticOverflowIsReported() =>
		Assert.Throws<OverflowException>(() => new Rational32(int.MaxValue) + new Rational32(1));
}