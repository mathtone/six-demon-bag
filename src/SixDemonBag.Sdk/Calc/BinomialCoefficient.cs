using System.Numerics;

namespace Six.Demon.Bag.Calc;

public static partial class Calculate {

	public static T BinomialCoefficient<T>(T n, T k) where T : IBinaryInteger<T> {
		if(n < T.Zero || k < T.Zero || k > n)
			return T.Zero;

		if(k == T.Zero || k == n)
			return T.One;

		var two = T.One + T.One;
		if(k > n / two)
			k = n - k;

		var result = T.One;

		for(var i = T.One; i <= k; i++) {
			var numerator = n - k + i;
			var denominator = i;

			var gcd = Gcd(numerator, denominator);
			numerator /= gcd;
			denominator /= gcd;

			gcd = Gcd(result, denominator);
			result /= gcd;
			denominator /= gcd;

			if(denominator != T.One)
				throw new InvalidOperationException("Internal reduction failure.");

			result = checked(result * numerator);
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