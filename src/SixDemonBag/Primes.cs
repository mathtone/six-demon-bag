using System.Numerics;

namespace SixDemonBag;

public static class Primes {
	public static IEnumerable<BigInteger> Sequence {
		get {
			var primes = new List<BigInteger>();

			for(var candidate = new BigInteger(2); ; candidate++) {
				var prime = true;
				foreach(var divisor in primes) {
					if(divisor * divisor > candidate)
						break;
					if(candidate % divisor == 0) {
						prime = false;
						break;
					}
				}

				if(prime) {
					primes.Add(candidate);
					yield return candidate;
				}
			}
		}
	}
}