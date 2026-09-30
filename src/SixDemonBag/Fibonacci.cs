using System.Numerics;

namespace SixDemonBag;

public static class Fibonacci {
	public static IEnumerable<BigInteger> Sequence {
		get {
			BigInteger previous = 0;
			BigInteger current = 1;

			while(true) {
				yield return previous;
				(previous, current) = (current, previous + current);
			}
		}
	}

	public static BigInteger Recursive(int n) {
		ArgumentOutOfRangeException.ThrowIfNegative(n);
		return n < 2 ? n : Recursive(n - 1) + Recursive(n - 2);
	}
}

public static class StairWalk {
	public static BigInteger Count(int height, int maxSteps) {
		ArgumentOutOfRangeException.ThrowIfNegative(height);
		ArgumentOutOfRangeException.ThrowIfLessThan(maxSteps, 1);

		var ways = new BigInteger[height + 1];
		ways[0] = 1;

		for(var stair = 1; stair <= height; stair++)
			for(var step = 1; step <= Math.Min(stair, maxSteps); step++)
				ways[stair] += ways[stair - step];

		return ways[height];
	}
}