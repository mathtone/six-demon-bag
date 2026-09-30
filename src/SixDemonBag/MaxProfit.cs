namespace SixDemonBag;

public static class MaxProfit {
	public static int Calculate(IReadOnlyList<int> prices, int? maxTransactions = null) {
		ArgumentNullException.ThrowIfNull(prices);
		if(maxTransactions < 0)
			throw new ArgumentOutOfRangeException(nameof(maxTransactions));
		if(prices.Any(price => price < 0))
			throw new ArgumentOutOfRangeException(nameof(prices), "Prices cannot be negative.");
		if(prices.Count < 2 || maxTransactions == 0)
			return 0;

		var transactions = maxTransactions ?? prices.Count / 2;
		if(transactions >= prices.Count / 2) {
			var profit = prices
				.Zip(prices.Skip(1), (left, right) => Math.Max(0L, (long)right - left))
				.Sum();
			return checked((int)profit);
		}

		var profits = new long[transactions + 1, prices.Count];

		for(var transaction = 1; transaction <= transactions; transaction++) {
			var bestPurchase = -(long)prices[0];

			for(var day = 1; day < prices.Count; day++) {
				profits[transaction, day] = Math.Max(
					profits[transaction, day - 1],
					prices[day] + bestPurchase);
				bestPurchase = Math.Max(
					bestPurchase,
					profits[transaction - 1, day] - prices[day]);
			}
		}

		return checked((int)profits[transactions, prices.Count - 1]);
	}
}