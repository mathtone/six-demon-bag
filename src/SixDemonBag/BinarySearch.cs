namespace SixDemonBag;

public static class BinarySearch {
	public static int Iterative<T>(
		IReadOnlyList<T> items,
		T value,
		IComparer<T>? comparer = null) {
		ArgumentNullException.ThrowIfNull(items);
		comparer ??= Comparer<T>.Default;

		var low = 0;
		var high = items.Count - 1;

		while(low <= high) {
			var middle = low + (high - low) / 2;
			var comparison = comparer.Compare(value, items[middle]);

			if(comparison == 0)
				return middle;

			if(comparison < 0)
				high = middle - 1;
			else
				low = middle + 1;
		}

		return -1;
	}

	public static int Recursive<T>(
		IReadOnlyList<T> items,
		T value,
		IComparer<T>? comparer = null) {
		ArgumentNullException.ThrowIfNull(items);
		comparer ??= Comparer<T>.Default;
		return Search(0, items.Count - 1);

		int Search(int low, int high) {
			if(low > high)
				return -1;

			var middle = low + (high - low) / 2;
			var comparison = comparer.Compare(value, items[middle]);

			return comparison switch {
				0 => middle,
				< 0 => Search(low, middle - 1),
				_ => Search(middle + 1, high)
			};
		}
	}
}