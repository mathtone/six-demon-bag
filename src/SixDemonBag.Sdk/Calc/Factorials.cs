using System.Numerics;

namespace Six.Demon.Bag.Calc;

public static partial class Calculate {

	public static BigInteger Factorial(int n) {
		n = ThrowIfNegative(n);
		if(n < factorials.Length) {
			return factorials[n];
		}
		
		var result = (BigInteger)factorials[^1];
		for(var i = factorials.Length; i <= n; i++)
			result *= i;
		return result;
	}

	public static int Factorial32(int n) =>
		ThrowIfNegative(n) > 12
			? throw new ArgumentOutOfRangeException(nameof(n), "Factorials greater than 12 do not fit in an int.")
			: (int)factorials[n];

	public static long Factorial64(int n) =>
		ThrowIfNegative(n) > 20
			? throw new ArgumentOutOfRangeException(nameof(n), "Factorials greater than 20 do not fit in a long.")
			: (long)factorials[n];

	public static Int128 Factorial128(int n) =>
		ThrowIfNegative(n) > 33
			? throw new ArgumentOutOfRangeException(nameof(n), "Factorials greater than 33 do not fit in an Int128.")
			: factorials[n];

	public static ReadOnlySpan<Int128> Factorials => factorials;
	private static readonly Int128[] factorials = CreateFactorials();

	private static int ThrowIfNegative(int n) =>
		n < 0
			? throw new ArgumentOutOfRangeException(nameof(n), "Factorial is undefined for negative integers.")
			: n;

	private static Int128[] CreateFactorials() {
		var values = new Int128[34];
		values[0] = 1;

		for(var i = 1; i < values.Length; i++) {
			values[i] = checked(values[i - 1] * i);
		}

		return values;
	}
}