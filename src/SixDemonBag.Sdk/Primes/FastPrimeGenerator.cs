using System.Numerics;
using System.Runtime.CompilerServices;

namespace Six.Demon.Bag.Primes;

public static class FastPrimeGenerator<T>
	where T : IBinaryInteger<T> {

	private static readonly T Two = T.CreateChecked(2);
	private static readonly T Three = T.CreateChecked(3);

	private static readonly T[] SeedPrimes = [.. PrimeGenerator<T>.GetPrimes().Take(64)];

	public static async IAsyncEnumerable<T> GetPrimesAsync(
		int segmentOddCount = 16384,
		int? degreeOfParallelism = null,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	) {
		ArgumentOutOfRangeException.ThrowIfLessThan(segmentOddCount, 1);

		var dop = degreeOfParallelism ?? Environment.ProcessorCount;
		ArgumentOutOfRangeException.ThrowIfLessThan(dop, 1);

		// Yield 2 separately. Keep only odd primes in the working list so
		// segment scans do not waste time checking candidate % 2.
		yield return Two;

		var knownPrimes = new List<T>(SeedPrimes.Length - 1);
		for(var i = 1; i < SeedPrimes.Length; i++) {
			var prime = SeedPrimes[i];
			knownPrimes.Add(prime);
			yield return prime;
		}

		var nextCandidate = checked(knownPrimes[^1] + Two);

		while(true) {
			cancellationToken.ThrowIfCancellationRequested();

			var last = knownPrimes[^1];
			if(!TryCheckedMultiply(last, last, out var safeEnd))
				yield break;

			nextCandidate = EnsureOddAtOrAbove(nextCandidate);

			if(nextCandidate > safeEnd)
				yield break;

			var waveSize = TryCreateWaveSize(segmentOddCount, dop, out var computedWaveSize)
				? computedWaveSize
				: safeEnd;

			var candidateEnd = TryCheckedAdd(nextCandidate, waveSize, out var computedCandidateEnd)
				? computedCandidateEnd
				: safeEnd;

			var waveEnd = EnsureOddAtOrBelow(Min(candidateEnd, safeEnd));

			if(nextCandidate > waveEnd) {
				if(!TryCheckedAdd(waveEnd, Two, out nextCandidate))
					yield break;

				continue;
			}

			var segments = BuildSegments(nextCandidate, waveEnd, segmentOddCount);
			var tasks = new Task<SegmentResult>[segments.Count];

			for(var i = 0; i < segments.Count; i++) {
				var segment = segments[i];
				tasks[i] = Task.Run(
					() => FindPrimesInSegment(segment.Index, segment.Start, segment.End, knownPrimes, cancellationToken),
					cancellationToken
				);
			}

			var results = await Task.WhenAll(tasks).ConfigureAwait(false);
			Array.Sort(results, static (a, b) => a.Index.CompareTo(b.Index));

			foreach(var result in results) {
				foreach(var prime in result.Primes) {
					knownPrimes.Add(prime);
					yield return prime;
				}
			}

			if(!TryCheckedAdd(waveEnd, Two, out nextCandidate))
				yield break;

			await Task.Yield();
		}
	}

	private static List<Segment> BuildSegments(T start, T end, int segmentOddCount) {
		var segments = new List<Segment>();
		var segmentWidth = checked(T.CreateChecked(segmentOddCount) * Two - T.One);

		var index = 0;
		var segmentStart = EnsureOddAtOrAbove(start);

		while(segmentStart <= end) {
			var segmentEnd = TryCheckedAdd(segmentStart, segmentWidth, out var candidateEnd)
				? Min(candidateEnd, end)
				: end;

			segmentEnd = EnsureOddAtOrBelow(segmentEnd);
			segments.Add(new Segment(index++, segmentStart, segmentEnd));

			if(!TryCheckedAdd(segmentEnd, Two, out segmentStart))
				break;
		}

		return segments;
	}

	private static SegmentResult FindPrimesInSegment(
		int index,
		T start,
		T end,
		IReadOnlyList<T> knownPrimes,
		CancellationToken cancellationToken
	) {
		var primes = new List<T>();

		for(var candidate = EnsureOddAtOrAbove(start); candidate <= end; candidate += Two) {
			cancellationToken.ThrowIfCancellationRequested();

			var isComposite = false;

			for(var i = 0; i < knownPrimes.Count; i++) {
				var prime = knownPrimes[i];

				if(prime > candidate / prime)
					break;

				if(candidate % prime == T.Zero) {
					isComposite = true;
					break;
				}
			}

			if(!isComposite)
				primes.Add(candidate);
		}

		return new SegmentResult(index, primes);
	}

	private static bool TryCreateWaveSize(int segmentOddCount, int degreeOfParallelism, out T waveSize) {
		try {
			waveSize = checked(
				T.CreateChecked(segmentOddCount) *
				T.CreateChecked(degreeOfParallelism) *
				Two -
				T.One);

			return true;
		}
		catch(OverflowException) {
			waveSize = T.Zero;
			return false;
		}
	}

	private static bool TryCheckedMultiply(T left, T right, out T result) {
		try {
			result = checked(left * right);
			return true;
		}
		catch(OverflowException) {
			result = T.Zero;
			return false;
		}
	}

	private static bool TryCheckedAdd(T left, T right, out T result) {
		try {
			result = checked(left + right);
			return true;
		}
		catch(OverflowException) {
			result = T.Zero;
			return false;
		}
	}

	private static T EnsureOddAtOrAbove(T value) =>
		(value & T.One) == T.Zero ? checked(value + T.One) : value;

	private static T EnsureOddAtOrBelow(T value) =>
		(value & T.One) == T.Zero ? value - T.One : value;

	private static T Min(T left, T right) =>
		left < right ? left : right;

	private readonly record struct Segment(int Index, T Start, T End);
	private readonly record struct SegmentResult(int Index, List<T> Primes);
}