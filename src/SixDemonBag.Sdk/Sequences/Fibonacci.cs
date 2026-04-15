
using System.Numerics;
namespace Six.Demon.Bag.Sequences;

public static class Fibonacci {
	public static IEnumerable<int> Sequence32=> GetSequence(0, 1);
	public static IEnumerable<long> Sequence64 => GetSequence<long>(0, 1);
	public static IEnumerable<Int128> Sequence128 => GetSequence<Int128>(0, 1);
	public static IEnumerable<BigInteger> Sequence => GetSequence<BigInteger>(0, 1);

	static IEnumerable<T> GetSequence<T>(T zero, T one)
		where T : IAdditionOperators<T, T, T> {
		
		T a = zero;
		T b = one;
		T c;

		yield return a;
		yield return b;

		while(true) {
			try {
				c = checked(a + b);
			}
			catch(OverflowException) {
				yield break;
			}
			a = b;
			b = c;
			yield return b;
		}
	}
}