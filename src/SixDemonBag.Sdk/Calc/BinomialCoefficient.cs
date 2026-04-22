using System.Numerics;

namespace Six.Demon.Bag.Calc;

public static partial class Calculate {

	public static T BinomialCoefficient<T>(T n, T k) where T : IBinaryInteger<T> {
		if(n < T.Zero || k < T.Zero || k > n)
			return T.Zero;

		if(k == T.Zero || k == n)
			return T.One;

		var two = T.CreateChecked(2);
		if(k > n / two)
			k = n - k;

		var result = T.One;

		for(var i = T.One; i <= k; i++) {
			var numerator = n - k + i;
			var denominator = i;

			var gcd1 = Gcd(numerator, denominator);
			numerator /= gcd1;
			denominator /= gcd1;

			var gcd2 = Gcd(result, denominator);
			result /= gcd2;
			denominator /= gcd2;

			result = checked(result * numerator);

			// denominator should now be 1 for exact integer arithmetic
			if(denominator != T.One)
				throw new InvalidOperationException("Binomial coefficient reduction did not fully eliminate the denominator.");
		}

		return result;
	}

	public static T Gcd<T>(T a, T b) where T : IBinaryInteger<T> {
		a = T.Abs(a);
		b = T.Abs(b);

		while(b != T.Zero) {
			(a, b) = (b, a % b);
		}

		return a;
	}
}