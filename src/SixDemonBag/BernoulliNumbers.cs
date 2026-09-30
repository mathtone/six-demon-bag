namespace SixDemonBag;

public static class BernoulliNumbers {
	public static IEnumerable<BigRational> Sequence {
		get {
			var values = new List<BigRational> { new(1, 1), new(-1, 2) };

			foreach(var value in values)
				yield return value;

			for(var n = 2; ; n++) {
				if(n % 2 != 0) {
					values.Add(default);
					yield return default;
					continue;
				}

				var sum = default(BigRational);
				for(var k = 0; k < n; k++)
					sum += new BigRational(BigMath.BinomialCoefficient(n + 1, k)) * values[k];

				var value = -sum / new BigRational(n + 1);
				values.Add(value);
				yield return value;
			}
		}
	}
}