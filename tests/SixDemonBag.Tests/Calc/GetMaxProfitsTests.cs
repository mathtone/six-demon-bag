using Six.Demon.Bag.Calc;

namespace Six.Demon.Bag.Tests.Calc;

public class GetMaxProfitsTests {

	[Theory]
	[InlineData(new[] { 10, 22, 5, 75, 65, 80 }, null, 97)]
	[InlineData(new[] { 10, 22, 5, 75, 65, 80 }, 2, 87)]
	[InlineData(new[] { 10, 22, 5, 75, 65, 80 }, 1, 75)]
	[InlineData(new[] { 1, 1, 1, 1 }, null, 0)]
	[InlineData(new[] { 1, 1, 3, 1 }, null, 2)]
	[InlineData(new[] { 1, 2, 3, 4 }, null, 3)]
	[InlineData(new[] { 1, 3, 13, 0 }, 1, 12)]
	public void GetMaxProfits(int[] prices, int? max, int expected) =>
		Assert.Equal(expected, Calculate.MaxProfit(prices, prices.Length, max ?? prices.Length));
}

public class PalindromesTests {

	[Theory]
	[InlineData("abcd", "dcbabcd")]
	[InlineData("abcdef", "fedcbabcdef")]
	[InlineData("aacecaaa", "aaacecaaa")]
	public void TestMethod(string input, string result) =>
		Assert.Equal(Calculate.ShortestPalindrome(input), result);


	[Theory]
	[InlineData("abcd", "dcbabcd")]
	[InlineData("abcdef", "fedcbabcdef")]
	[InlineData("aacecaaa", "aaacecaaa")]
	public void TestMethod2(string input, string result) =>
		Assert.Equal(Calculate.ShortestPalindromeFast(input), result);
}