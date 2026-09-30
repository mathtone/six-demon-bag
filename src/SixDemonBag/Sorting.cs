namespace SixDemonBag;

public static class Sorting {
	public static void Bubble<T>(IList<T> items, IComparer<T>? comparer = null) {
		ArgumentNullException.ThrowIfNull(items);
		comparer ??= Comparer<T>.Default;

		for(var end = items.Count - 1; end > 0; end--)
			for(var i = 0; i < end; i++)
				if(comparer.Compare(items[i], items[i + 1]) > 0)
					Swap(items, i, i + 1);
	}

	public static void Quick<T>(IList<T> items, IComparer<T>? comparer = null) {
		ArgumentNullException.ThrowIfNull(items);
		comparer ??= Comparer<T>.Default;
		if(items.Count > 1)
			Sort(0, items.Count - 1);

		void Sort(int low, int high) {
			var left = low;
			var right = high;
			var pivot = items[low + (high - low) / 2];

			while(left <= right) {
				while(comparer.Compare(items[left], pivot) < 0)
					left++;
				while(comparer.Compare(items[right], pivot) > 0)
					right--;
				if(left <= right)
					Swap(items, left++, right--);
			}

			if(low < right)
				Sort(low, right);
			if(left < high)
				Sort(left, high);
		}
	}

	public static void Heap<T>(IList<T> items, IComparer<T>? comparer = null) {
		ArgumentNullException.ThrowIfNull(items);
		comparer ??= Comparer<T>.Default;

		for(var i = items.Count / 2 - 1; i >= 0; i--)
			Heapify(items.Count, i);

		for(var end = items.Count - 1; end > 0; end--) {
			Swap(items, 0, end);
			Heapify(end, 0);
		}

		void Heapify(int count, int root) {
			var largest = root;
			var left = 2 * root + 1;
			var right = left + 1;

			if(left < count && comparer.Compare(items[left], items[largest]) > 0)
				largest = left;
			if(right < count && comparer.Compare(items[right], items[largest]) > 0)
				largest = right;
			if(largest == root)
				return;

			Swap(items, root, largest);
			Heapify(count, largest);
		}
	}

	private static void Swap<T>(IList<T> items, int left, int right) =>
		(items[left], items[right]) = (items[right], items[left]);
}