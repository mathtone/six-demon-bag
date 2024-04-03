namespace GetMaxProfit {
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
			Assert.Equal(expected, GetMaxProfit(prices, prices.Length, max ?? prices.Length));

		static int GetMaxProfit(int[] prices, int n, int k) {

			if (n == 0 || k == 0)
				return 0;

			var profit = new int[k + 1, n];

			for (int i = 1; i <= k; i++) {
				int maxDiff = -prices[0];

				for (int j = 1; j < n; j++) {
					profit[i, j] = Math.Max(profit[i, j - 1], prices[j] + maxDiff);
					maxDiff = Math.Max(maxDiff, profit[i - 1, j] - prices[j]);
				}
			}

			return profit[k, n - 1];
		}
	}
}