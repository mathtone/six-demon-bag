namespace Sorting {
	public class SortingTests {

		public static int[] GetUnsortedValues(int count) {
			var rtn = Enumerable.Range(0, count).ToArray();
			rtn.UnSort();
			return rtn;
		}

		static void Test_Sort(Action<IList<int>> sortMethod) {
			var values = GetUnsortedValues(10);
			sortMethod(values);
			Assert.Equal(Enumerable.Range(0, 10), values);
		}

		[Fact]
		public void TestBubbleSort() => Test_Sort(BubbleSort.Sort);

		[Fact]
		public void TestQuickSort() => Test_Sort(QuickSort.Sort);

		[Fact]
		public void TestHeapSort() => Test_Sort(HeapSort.Sort);
	}

	public static class CollectionExtensions {
		public static void Swap<T>(this IList<T> items, int a, int b) {
			var t = items[a];
			items[a] = items[b];
			items[b] = t;
		}

		public static void UnSort<T>(this IList<T> items) {
			var rng = new Random();
			for (var i = items.Count; i > 1; i--) {
				items.Swap(rng.Next(i), i - 1);
			}
		}

		public static IEnumerable<T> Randomize<T>(this IList<T> items) {

			var indices = Enumerable.Range(0, items.Count).ToArray();
			indices.UnSort();
			return indices.Select(i => items[i]);
		}
	}

	public class HeapSort {
		public static void Sort<T>(IList<T> items)
			where T : IComparable<T> =>
			Sort(items, Comparer<T>.Default);

		public static void Sort<T>(IList<T> items, Comparer<T> comparer) {

			var l = items.Count;

			for (var i = l / 2 - 1; i >= 0; i--)
				BuildHeap(items, l, i, comparer);

			for (var i = l - 1; i >= 0; i--) {
				items.Swap(0, i);
				BuildHeap(items, i, 0, comparer);
			}
		}
		static void BuildHeap<T>(IList<T> items, int n, int i, Comparer<T> comparer) {

			var largest = i;
			var left = 2 * i + 1;
			var right = 2 * i + 2;

			if (left < n && comparer.Compare(items[left], items[largest]) > 0)
				largest = left;

			if (right < n && comparer.Compare(items[right], items[largest]) > 0)
				largest = right;

			if (largest != i) {
				items.Swap(i, largest);
				BuildHeap(items, n, largest, comparer);
			}
		}
	}

	public class QuickSort {

		public static void Sort<T>(IList<T> items)
			where T : IComparable<T> =>
			Sort(items, Comparer<T>.Default);

		public static void Sort<T>(IList<T> items, IComparer<T> comparer) =>
			Sort(items, 0, items.Count - 1, comparer);

		static void Sort<T>(IList<T> items, int min, int max, IComparer<T> comparer) {
			int a = min, b = max;
			var p = items[(a + b) / 2];

			while (a <= b) {
				while (comparer.Compare(items[a], p) < 0) a++;
				while (comparer.Compare(items[b], p) > 0) b--;
				if (a <= b) items.Swap(a++, b--);
			}

			if (min < b) Sort(items, min, b, comparer);
			if (a < max) Sort(items, a, max, comparer);
		}
	}

	public class BubbleSort {
		public static void Sort<T>(IList<T> items) where T : IComparable<T> {
			for (var j = 0; j < items.Count - 1; j++) {
				for (var i = 0; i < items.Count - 1; i++) {
					if (items[i].CompareTo(items[i + 1]) > 0) {
						items.Swap(i, i + 1);
					}
				}
			}
		}
	}
}