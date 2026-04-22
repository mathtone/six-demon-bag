using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

using Six.Demon.Bag.Primes;
using Six.Demon.Bag.Sequences;
using Six.Demon.Bag.Utilities;


//var c = Primes<short>.GetPrimes().Last();
var n = BernoulliNumbers<int>.Sequence.Take(20).ToArray();
Console.WriteLine(string.Join(',',n));

//public static class Primes<T> where T: INumber<T> {

//	static readonly T One = T.One;
//	static readonly T Two = T.CreateChecked(2);

//	public static IEnumerable<T> GetPrimes() {

//		var primes = new List<T> { Two };
//		T current = One;

//		while(true) {
//			try {
//				current = checked(current + Two);
//			}
//			catch(OverflowException) {
//				yield break;
//			}

//			var isComposite = false;

//			foreach(var p in primes) {
//				if(p > current / p)
//					break;

//				if(current % p == T.Zero) {
//					isComposite = true;
//					break;
//				}
//			}

//			if(!isComposite) {
//				primes.Add(current);
//				yield return current;
//			}
//		}
//	}
//}