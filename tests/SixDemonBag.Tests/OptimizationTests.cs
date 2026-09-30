namespace SixDemonBag.Tests;

public class OptimizationTests {
	[Theory]
	[InlineData(new[] { 10, 22, 5, 75, 65, 80 }, null, 97)]
	[InlineData(new[] { 10, 22, 5, 75, 65, 80 }, 2, 87)]
	[InlineData(new[] { 10, 22, 5, 75, 65, 80 }, 1, 75)]
	[InlineData(new[] { 1, 1, 1, 1 }, null, 0)]
	[InlineData(new[] { 1, 3, 13, 0 }, 1, 12)]
	[InlineData(new int[0], null, 0)]
	public void MaximumProfitIsCalculated(int[] prices, int? transactions, int expected) =>
		Assert.Equal(expected, MaxProfit.Calculate(prices, transactions));

	[Fact]
	public void NegativeTransactionLimitIsRejected() =>
		Assert.Throws<ArgumentOutOfRangeException>(() => MaxProfit.Calculate([1, 2], -1));

	[Fact]
	public void NegativePricesAreRejected() =>
		Assert.Throws<ArgumentOutOfRangeException>(() => MaxProfit.Calculate([1, -1]));

	[Fact]
	public void ProfitOverflowIsReported() =>
		Assert.Throws<OverflowException>(() => MaxProfit.Calculate([0, int.MaxValue, 0, int.MaxValue]));

	[Theory]
	[InlineData(new[] { 1, 2, 3, 4, 5 }, 15)]
	[InlineData(new[] { 30, -99, 3, 4, 25, -1 }, 32)]
	[InlineData(new[] { 30, -99, 100, -101, 25, -1 }, 100)]
	[InlineData(new[] { -5, -2, -8 }, -2)]
	public void MaximumSubarraySumIsCalculated(int[] values, int expected) =>
		Assert.Equal(expected, MaximumSubarray.Sum(values));

	[Fact]
	public void EmptyMaximumSubarrayIsRejected() =>
		Assert.Throws<ArgumentException>(() => MaximumSubarray.Sum([]));
}