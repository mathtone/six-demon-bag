using System.Numerics;

namespace Six.Demon.Bag.Sequences;

public static class PrimeGenerator<T>
	where T : INumber<T> {
	private static readonly T Two = T.CreateChecked(2);

	public static IEnumerable<T> GetPrimes() {
		yield return Two;

		var primes = new List<T> { Two };
		var current = T.One;

		while(true) {
			try {
				current = checked(current + Two);
			}
			catch(OverflowException) {
				yield break;
			}

			var isComposite = false;

			foreach(var p in primes) {
				if(p > current / p)
					break;

				if(current % p == T.Zero) {
					isComposite = true;
					break;
				}
			}

			if(!isComposite) {
				primes.Add(current);
				yield return current;
			}
		}
	}
}