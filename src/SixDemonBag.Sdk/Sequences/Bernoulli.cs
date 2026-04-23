using System.Numerics;

using Six.Demon.Bag.Calc;
using Six.Demon.Bag.Rationals;

namespace Six.Demon.Bag.Sequences;

public static class BernoulliNumbers<T> where T : IBinaryInteger<T> {

	public static IEnumerable<Rational<T>> Sequence => GenerateBernoulliNumbers();

	private static IEnumerable<Rational<T>> GenerateBernoulliNumbers() {
		var cache = new List<Rational<T>> {
			new(T.One, T.One),
			new(T.CreateChecked(-1), T.CreateChecked(2))
		};

		yield return cache[0];
		yield return cache[1];

		for(var n = 2; true; n++) {
			if((n & 1) != 0) {
				var zero = new Rational<T>(T.Zero, T.One);
				cache.Add(zero);
				yield return zero;
				continue;
			}

			var sum = new Rational<T>(T.Zero, T.One);
			var tn = T.CreateChecked(n);
			var nPlusOne = tn + T.One;

			for(var k = 0; k < n; k++) {
				if(k > 1 && (k & 1) != 0)
					continue;

				Rational<T> binom = Calculate.BinomialCoefficient(nPlusOne, T.CreateChecked(k));
				sum += binom * cache[k];
			}

			var bn = -sum / nPlusOne;
			cache.Add(bn);
			yield return bn;
		}
	}
}