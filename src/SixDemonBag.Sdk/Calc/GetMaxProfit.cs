using System.Numerics;

namespace Six.Demon.Bag.Calc;

public static partial class Calculate {

	public static T MaxProfit<T>(T[] prices, int n, int k) where T : INumber<T> {
		ArgumentNullException.ThrowIfNull(prices);
		ArgumentOutOfRangeException.ThrowIfNegative(n);
		ArgumentOutOfRangeException.ThrowIfNegative(k);

		if(n > prices.Length)
			throw new ArgumentOutOfRangeException(nameof(n), "n cannot exceed prices.Length.");

		if(n == 0 || k == 0)
			return T.Zero;

		if(n == 1)
			return T.Zero;

		if(k >= n / 2) {
			var total = T.Zero;

			for(var i = 1; i < n; i++) {
				var diff = checked(prices[i] - prices[i - 1]);
				if(diff > T.Zero)
					total = checked(total + diff);
			}

			return total;
		}

		var profit = new T[k + 1, n];

		for(var i = 1; i <= k; i++) {
			var maxDiff = checked(-prices[0]);

			for(var j = 1; j < n; j++) {
				profit[i, j] = T.Max(
					profit[i, j - 1],
					checked(prices[j] + maxDiff));

				maxDiff = T.Max(
					maxDiff,
					checked(profit[i - 1, j] - prices[j]));
			}
		}

		return profit[k, n - 1];
	}

	public static T MaxProfit<T>(T[] prices, int k) where T : INumber<T> =>
		MaxProfit(prices, prices?.Length ?? 0, k);
}