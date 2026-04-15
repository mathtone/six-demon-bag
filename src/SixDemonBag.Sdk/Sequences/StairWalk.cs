
using System.Numerics;
namespace Six.Demon.Bag.Sequences;
public static class StairWalk {
	public static IEnumerable<BigInteger> FibonacciSequence => Walk(2);

	public static BigInteger GetWalksToTop(int height, int maxSteps) =>
		Walk(maxSteps).ElementAt(height + 1);

	public static IEnumerable<BigInteger> Walk(int maxSteps) {

		var r = new BigInteger[maxSteps + 1];
		yield return r[0] = 0;
		yield return r[1] = 1;

		for(var i = 2; i < maxSteps; i++) {
			r[i] = r[0..i].Sum();
			yield return r[i];
		}

		while(true) {
			r[maxSteps] = 0;
			for(var i = 0U; i < maxSteps; i++) {
				r[maxSteps] += r[i];
				r[i] = r[i + 1];
			}
			yield return r[maxSteps];
		}
	}
}