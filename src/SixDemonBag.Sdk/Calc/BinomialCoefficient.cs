using System.Numerics;

namespace Six.Demon.Bag.Calc;

public static partial class Calculate {
	public static BigInteger BinomialCoefficient(int n, int k) {
		if(k < 0 || k > n)
			return BigInteger.Zero;

		if(k == 0 || k == n)
			return BigInteger.One;

		if(k > n / 2)
			k = n - k;

		var result = BigInteger.One;

		for(var i = 1; i <= k; i++) {
			var numerator = n - k + i;
			result *= numerator;
			result /= i;
		}

		return result;
	}
}