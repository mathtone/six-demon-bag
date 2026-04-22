using System.Numerics;

namespace Six.Demon.Bag.Primes;

public static class PrimeGenerator<T>
	where T : INumber<T> {
	private static readonly T Two = T.CreateChecked(2);


	public static IEnumerable<T> GetPrimes() {

		var primes = new List<T>();
		foreach(var candidate in PrimeCandidates<T>.GetCandidates()) {
			var isComposite = false;
			foreach(var p in primes) {
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
		}
		//yield return Two;

		//var primes = new List<T> { Two };
		//var current = T.One;

		//while(true) {
		//	try {
		//		current = checked(current + Two);
		//	}
		//	catch(OverflowException) {
		//		yield break;
		//	}

		//	var isComposite = false;

		//	foreach(var p in primes) {
		//		if(p > current / p)
		//			break;

		//		if(current % p == T.Zero) {
		//			isComposite = true;
		//			break;
		//		}
		//	}

		//	if(!isComposite) {
		//		primes.Add(current);
		//		yield return current;
		//	}
		//}
	}


}

public static class PrimeCandidates<T> where T : INumber<T> {
	private static readonly T Two = T.CreateChecked(2);
	private static readonly T Four = T.CreateChecked(4);

	public static IEnumerable<T> GetCandidates() {
		yield return Two;
		yield return T.CreateChecked(3);
		yield return T.CreateChecked(5);

		var current = T.CreateChecked(7);
		yield return current;
		while(true) {
			try {
				current = checked(current + Two);
			}
			catch(OverflowException) {
				yield break;
			}
			yield return current;

			try {
				current = checked(current + Two);
			}
			catch(OverflowException) {
				yield break;
			}
			yield return current;

			try {
				current = checked(current + Two);
			}
			catch(OverflowException) {
				yield break;
			}
			yield return current;
			try {
				current = checked(current + Four);
			}
			catch(OverflowException) {
				yield break;
			}
			yield return current;
			//yield return checked(current += Two);
			//yield return checked(current += Two);
			//yield return checked(current += Two);
			//yield return checked(current += Four);
		}
	}
}