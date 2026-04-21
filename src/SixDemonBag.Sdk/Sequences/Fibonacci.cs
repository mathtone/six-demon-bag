using System.Collections.ObjectModel;
using System.Numerics;

namespace Six.Demon.Bag.Sequences;

public static class Fibonacci {

	private static readonly Lazy<ReadOnlyCollection<byte>> sequence8 =Create<byte>();
	private static readonly Lazy<ReadOnlyCollection<short>> sequence16 = Create<short>();
	private static readonly Lazy<ReadOnlyCollection<int>> sequence32 =Create<int>();
	private static readonly Lazy<ReadOnlyCollection<long>> sequence64 = Create<long>();
	private static readonly Lazy<ReadOnlyCollection<Int128>> sequence128 = Create<Int128>();

	private static Lazy<ReadOnlyCollection<T>> Create<T> ()
		where T : INumber<T> =>
		new(() => new([.. Get<T>()]));

	public static IReadOnlyList<byte> Sequence8 => sequence8.Value;
	public static IReadOnlyList<short> Sequence16 => sequence16.Value;
	public static IReadOnlyList<int> Sequence32 => sequence32.Value;
	public static IReadOnlyList<long> Sequence64 => sequence64.Value;
	public static IReadOnlyList<Int128> Sequence128 => sequence128.Value;

	// The BigInteger sequence is not practically bounded, so we expose it lazily.
	public static IEnumerable<BigInteger> Sequence => Get<BigInteger>();

	public static IEnumerable<T> Get<T>() where T : INumber<T> {
		yield return T.Zero;

		foreach(var n in StairWalk<T>.GetSequence(2))
			yield return n;
	}
}
