using System.Collections;

namespace Six.Demon.Bag.Lists;

public sealed class LargeListSegmentView<T> : IReadOnlyLargeList<T> {
	private readonly IReadOnlyLargeList<T> source;
	private readonly long start;

	public long Count { get; }

	public LargeListSegmentView(IReadOnlyLargeList<T> source, long start, long count) {
		ArgumentNullException.ThrowIfNull(source);

		if(start < 0 || start > source.Count)
			throw new ArgumentOutOfRangeException(nameof(start));

		if(count < 0 || count > source.Count - start)
			throw new ArgumentOutOfRangeException(nameof(count));

		this.source = source;
		this.start = start;
		Count = count;
	}

	public T this[long index] {
		get {
			if(index < 0 || index >= Count)
				throw new ArgumentOutOfRangeException(nameof(index));

			return source[start + index];
		}
	}

	public bool Contains(T item) => IndexOf(item) >= 0;

	public long IndexOf(T item) {
		var comparer = EqualityComparer<T>.Default;

		for(var i = 0L; i < Count; i++) {
			if(comparer.Equals(source[start + i], item))
				return i;
		}

		return -1;
	}

	public IEnumerator<T> GetEnumerator() {
		for(var i = 0L; i < Count; i++)
			yield return source[start + i];
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
