using System.Numerics;

namespace SixDemonBag.Tests;

public class BigMathTests {
	[Theory]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	[InlineData(120, 5)]
	public void FactorialReturnsExpectedValue(int expected, int n) =>
		Assert.Equal(new BigInteger(expected), BigMath.Factorial(n));

	[Theory]
	[InlineData(45, 10, 2)]
	[InlineData(1326, 52, 2)]
	[InlineData(2598960, 52, 5)]
	public void BinomialCoefficientReturnsExpectedValue(int expected, int n, int k) =>
		Assert.Equal(new BigInteger(expected), BigMath.BinomialCoefficient(n, k));

	[Fact]
	public void BinomialCoefficientIsSymmetric() =>
		Assert.Equal(
			BigMath.BinomialCoefficient(100, 3),
			BigMath.BinomialCoefficient(100, 97));

	[Fact]
	public void NegativeFactorialIsRejected() =>
		Assert.Throws<ArgumentOutOfRangeException>(() => BigMath.Factorial(-1));

	[Theory]
	[InlineData(-1, 0)]
	[InlineData(2, -1)]
	[InlineData(2, 3)]
	public void InvalidBinomialArgumentsAreRejected(int n, int k) =>
		Assert.Throws<ArgumentOutOfRangeException>(() => BigMath.BinomialCoefficient(n, k));

	[Fact]
	public void FixedWidthFactorialOverflowIsReported() =>
		Assert.Throws<OverflowException>(() => BigMath.Factorial32(13));

	[Fact]
	public void FixedWidthBinomialOverflowIsReported() =>
		Assert.Throws<OverflowException>(() => BigMath.BinomialCoefficient32(34, 17));
}