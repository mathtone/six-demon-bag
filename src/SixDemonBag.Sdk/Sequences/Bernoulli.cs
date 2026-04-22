using System.Numerics;

using Six.Demon.Bag.Calc;
using Six.Demon.Bag.Rationals;

namespace Six.Demon.Bag.Sequences;

public static class BernoulliNumbers<T> where T : IBinaryInteger<T> {
	
	public static IEnumerable<Rational<T>> Sequence =>
		GenerateBernoulliNumbers();

	private static IEnumerable<Rational<T>> GenerateBernoulliNumbers() {
		var cache = new List<Rational<T>> {
			new(T.One,T.One),
			new(T.CreateChecked(-1), T.CreateChecked(2))
		};
		yield return cache[0];
		yield return cache[1];
		for(var n = 2; true; n++) {
			if(n % 2 != 0) {
				cache.Add(new(T.Zero, T.One));
				yield return cache[^1];
				continue;
			}
			var sum = new Rational<T>(T.Zero, T.One);
			var tn = T.CreateChecked(n);
			for(var k = 0; k < n; k++) {
				var binom = new Rational<T>(Calculate.BinomialCoefficient(tn + T.One, T.CreateChecked(k)), T.One);
				sum += binom * cache[k];
			}
			var bn = new Rational<T>(-sum.Numerator, sum.Denominator * (tn + T.One));
			cache.Add(bn);
			yield return bn;
		}
	}
}

//public static class BernoulliNumbers {

//	public static IEnumerable<Rational<BigInteger>> Sequence =>
//		GenerateBernoulliNumbers();

//	private static IEnumerable<Rational<BigInteger>> GenerateBernoulliNumbers() {

//		var cache = new List<Rational<BigInteger>> {
//			new(1,1),
//			new(-1, 2)
//		};

//		yield return cache[0];
//		yield return cache[1];

//		for(int n = 2; true; n++) {
//			if(n % 2 != 0) {
//				cache.Add(new(0, 1));
//				yield return cache[^1];
//				continue;
//			}

//			var sum = new Rational<BigInteger>(0, 1);
//			for(int k = 0; k < n; k++) {
//				var binom = new Rational<BigInteger>(Calculate.BinomialCoefficient(n + 1, k),1);
//				sum += binom * cache[k];
//			}

//			var bn = new BigRational(-1) * sum / new BigRational(n + 1);
//			cache.Add(bn);
//			yield return bn;
//		}
//	}
//}