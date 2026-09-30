using System.Numerics;

namespace SixDemonBag;

public static class BigMath {
	public static BigInteger Factorial(int n) {
		ArgumentOutOfRangeException.ThrowIfNegative(n);

		var result = BigInteger.One;
		for(var i = 2; i <= n; i++)
			result *= i;

		return result;
	}

	public static int Factorial32(int n) => checked((int)Factorial(n));

	public static long Factorial64(int n) => checked((long)Factorial(n));

	public static BigInteger BinomialCoefficient(int n, int k) {
		ArgumentOutOfRangeException.ThrowIfNegative(n);
		ArgumentOutOfRangeException.ThrowIfNegative(k);
		if(k > n)
			throw new ArgumentOutOfRangeException(nameof(k), "k cannot exceed n.");

		k = Math.Min(k, n - k);
		var result = BigInteger.One;

		for(var i = 1; i <= k; i++)
			result = result * (n - k + i) / i;

		return result;
	}

	public static int BinomialCoefficient32(int n, int k) =>
		checked((int)BinomialCoefficient(n, k));

	public static long BinomialCoefficient64(int n, int k) =>
		checked((long)BinomialCoefficient(n, k));
}