using System.Numerics;

namespace Six.Demon.Bag.Primes;

public static class PrimeGenerator<T>
	where T : IBinaryInteger<T> {
	private static readonly T Two = T.CreateChecked(2);
	private static readonly T Three = T.CreateChecked(3);

	public static IEnumerable<T> GetPrimes() {
		yield return Two;

		var primes = new List<T>();
		var candidate = Three;

		while(true) {
			var isComposite = false;
			var count = primes.Count;
			for(var i = 0; i < count; i++) {
				var p = primes[i];

				if(p > candidate / p)
					break;

				if(candidate % p == T.Zero) {
					isComposite = true;
					break;
				}
			}

			if(!isComposite) {
				primes.Add(candidate);
				yield return candidate;
			}

			try {
				candidate = checked(candidate + Two);
			}
			catch(OverflowException) {
				yield break;
			}
		}
	}
}