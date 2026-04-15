using Six.Demon.Bag.Calc;
using Six.Demon.Bag.Rationals;

namespace Six.Demon.Bag.Sequences;



public static class BernoulliNumbers {

	public static IEnumerable<BigRational> Sequence => GenerateBernoulliNumbers();

	private static IEnumerable<BigRational> GenerateBernoulliNumbers() {

		var cache = new List<BigRational> {
			new(1),
			new(-1, 2)
		};

		yield return cache[0];
		yield return cache[1];

		for(int n = 2; true; n++) {
			if(n % 2 != 0) {
				cache.Add(new(0));
				yield return cache.Last();
				continue;
			}

			var sum = new BigRational(0);
			for(int k = 0; k < n; k++) {
				var binom = new BigRational(Calculate.BinomialCoefficient(n + 1, k));
				sum += binom * cache[k];
			}

			var bn = new BigRational(-1) * sum / new BigRational(n + 1);
			cache.Add(bn);
			yield return bn;
		}
	}
}
