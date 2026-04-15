using System.Numerics;

using Six.Demon.Bag.Calc;

namespace Six.Demon.Bag.Tests.Calc;

public class BinomialCoefficientTests {
	[Theory]
	[InlineData(5, 1, 5)]
	[InlineData(5, 2, 10)]
	[InlineData(5, 3, 10)]
	[InlineData(5, 4, 5)]
	[InlineData(52, 2, 1326)]
	[InlineData(52, 5, 2598960)]
	public void TestBinomialCoefficient(int n, int k, BigInteger expected) =>
		Assert.Equal(expected, Calculate.BinomialCoefficient(n, k));
}