namespace Six.Demon.Bag.Lists;

public static class LargeListExtensions {

	public static IReadOnlyLargeList<T> GetSegment<T>(this IReadOnlyLargeList<T> list, long index, long count) =>
		new LargeListSegmentView<T>(list, index, count);

	public static IReadOnlyList<T> GetIndexableSegment<T>(this IReadOnlyLargeList<T> list, long index, int count) =>
		new ListSegmentView<T>(list, index, count);

	public static IEnumerable<IReadOnlyLargeList<T>> GetSegmentViews<T>(this IReadOnlyLargeList<T> list, long segmentSize) {
		if(segmentSize <= 0)
			throw new ArgumentOutOfRangeException(nameof(segmentSize), "Segment size must be positive.");

		var i = 0L;
		while(i < list.Count) {
			var segmentLength = Math.Min(segmentSize, list.Count - i);
			yield return list.GetSegment(i, segmentLength);
			i += segmentLength;
		}
	}
	public static IEnumerable<IReadOnlyList<T>> GetIndexableSegmentViews<T>(this IReadOnlyLargeList<T> list) =>
		list.GetIndexableSegmentViews(Array.MaxLength);

	public static IEnumerable<IReadOnlyList<T>> GetIndexableSegmentViews<T>(this IReadOnlyLargeList<T> list, int segmentSize) {
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(segmentSize);

		var i = 0L;
		while(i < list.Count) {
			var segmentLength = (int)Math.Min(segmentSize, list.Count - i);
			yield return list.GetIndexableSegment(i, segmentLength);
			i += segmentLength;
		}
	}

	public static IEnumerable<IEnumerable<T>> GetSegments<T>(this IReadOnlyLargeList<T> list, long segmentSize) =>
		list.GetSegmentViews(segmentSize);

	public static IEnumerable<IEnumerable<T>> GetIndexableSegments<T>(this IReadOnlyLargeList<T> list) =>
		list.GetIndexableSegmentViews();

	public static IEnumerable<IEnumerable<T>> GetIndexableSegments<T>(this IReadOnlyLargeList<T> list, int segmentSize) =>
		list.GetIndexableSegmentViews(segmentSize);
}