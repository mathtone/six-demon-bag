using System.Collections.ObjectModel;
using System.Numerics;

namespace Six.Demon.Bag.Sequences;

public static class Fibonacci {
	private static readonly Lazy<ReadOnlyCollection<byte>> sequence8 = Create<byte>();
	private static readonly Lazy<ReadOnlyCollection<short>> sequence16 = Create<short>();
	private static readonly Lazy<ReadOnlyCollection<int>> sequence32 = Create<int>();
	private static readonly Lazy<ReadOnlyCollection<long>> sequence64 = Create<long>();
	private static readonly Lazy<ReadOnlyCollection<Int128>> sequence128 = Create<Int128>();

	private static Lazy<ReadOnlyCollection<T>> Create<T>()
		where T : IBinaryInteger<T> =>
		new(() => new([.. GetSequence<T>()]));

	public static IReadOnlyList<byte> Sequence8 => sequence8.Value;
	public static IReadOnlyList<short> Sequence16 => sequence16.Value;
	public static IReadOnlyList<int> Sequence32 => sequence32.Value;
	public static IReadOnlyList<long> Sequence64 => sequence64.Value;
	public static IReadOnlyList<Int128> Sequence128 => sequence128.Value;

	public static IEnumerable<BigInteger> Sequence => GetSequence<BigInteger>();

	public static IEnumerable<T> GetSequence<T>() where T : IBinaryInteger<T> {
		yield return T.Zero;

		foreach(var n in StairCase<T>.GetSequence(2))
			yield return n;
	}

	public static T GetNthNumber<T>(int position) where T : IBinaryInteger<T> {
		ArgumentOutOfRangeException.ThrowIfNegative(position);

		var a = T.Zero;
		var b = T.One;

		if(position == 0)
			return a;

		for(var i = 1; i < position; i++) {
			var next = checked(a + b);
			a = b;
			b = next;
		}

		return b;
	}

#if DEBUG
	// Never use this in production. Demonstration/testing only.
	public static int RecursiveCalc(int n) =>
		(n < 2)
			? n
			: checked(RecursiveCalc(n - 1) + RecursiveCalc(n - 2));
#endif
}