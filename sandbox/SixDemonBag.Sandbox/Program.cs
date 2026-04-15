using Six.Demon.Bag.Calc;
using Six.Demon.Bag.Rationals;
using Six.Demon.Bag.Sequences;


//foreach(var i in Fibonacci.Sequence32) {
	Console.WriteLine(Fibonacci.Sequence128.Count());
//}

//foreach(var bn in GenerateBernoulliNumbers().Take(35)) {
//	Console.WriteLine(bn);
//}

//static IEnumerable<BigRational> GenerateBernoulliNumbers() {
//	var cache = new List<BigRational> {
//		BigRational.One,
//		new(-1, 2)
//	};

//	foreach(var b in cache)
//		yield return b;

//	for(int n = 2; ; n++) {
//		if((n & 1) == 1) {
//			cache.Add(BigRational.Zero);
//			yield return BigRational.Zero;
//			continue;
//		}

//		var sum = BigRational.Zero;

//		for(int k = 0; k < n; k++) {
//			sum += Calculate.BinomialCoefficient(n + 1, k) * cache[k];
//		}

//		var bn = -sum / (n + 1);
//		cache.Add(bn);
//		yield return bn;
//	}
//}