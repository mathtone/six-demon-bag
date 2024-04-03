using BigMath;
using System.Numerics;
using Xunit.Abstractions;

namespace BernoulliNumbers {
	public class BernoulliNumbersTests(ITestOutputHelper output) {

		private readonly ITestOutputHelper output = output;

		[Fact]
		public void Test1() {
			var bernoulli = GenerateBernoulliNumbers().Take(99).ToList();
			foreach (var n in bernoulli) {
				output.WriteLine(n.ToString());
			}
			Assert.Equal("67908260672905495624051117546403605607342195728504487509073961249992947058239/6", bernoulli[^1].ToString());
		}

		public static IEnumerable<BigRational> GenerateBernoulliNumbers() {

			var cache = new List<BigRational> {
				new(1),
				new(-1, 2)
			};

			yield return cache[0];
			yield return cache[1];

			for (int n = 2; true; n++) {
				if (n % 2 != 0) {
					cache.Add(new(0));
					yield return cache.Last();
					continue;
				}

				var sum = new BigRational(0);
				for (int k = 0; k < n; k++) {
					var binom = new BigRational(BigMaths.BinomialCoefficient(n + 1, k));
					sum += binom * cache[k];
				}

				var bn = new BigRational(-1) * sum / new BigRational(n + 1);
				cache.Add(bn);
				yield return bn;
			}
		}
	}
}