namespace BinarySearch {

	public class BinarySearchTests {

		private readonly IList<int> sortedList = new List<int> { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19 };
		private readonly IComparer<int> comparer = Comparer<int>.Default;

		[Fact]
		public void IterativeSearch_FindsElement_ReturnsCorrectIndex() {
			var index = BinarySearch.Iterative(sortedList, 11, comparer);
			Assert.Equal(5, index);
		}

		[Fact]
		public void IterativeSearch_ElementNotFound_ReturnsMinusOne() {
			var index = BinarySearch.Iterative(sortedList, 2, comparer);
			Assert.Equal(-1, index);
		}

		[Fact]
		public void RecursiveSearch_FindsElement_ReturnsCorrectIndex() {
			var index = BinarySearch.Recursive(sortedList, 11, 0, sortedList.Count - 1, comparer);
			Assert.Equal(5, index);
		}

		[Fact]
		public void RecursiveSearch_ElementNotFound_ReturnsMinusOne() {
			var index = BinarySearch.Recursive(sortedList, 2, 0, sortedList.Count - 1, comparer);
			Assert.Equal(-1, index);
		}
	}

	public class BinarySearch {

		public static int Iterative<T>(IList<T> items, T key, IComparer<T> comparer) {

			var min = 0;
			var max = items.Count - 1;

			while (min <= max) {
				var mid = min + (max - min) / 2;
				var c = comparer.Compare(key, items[mid]);

				if (c == 0)
					return mid;
				else if (c < 0)
					max = mid - 1;
				else
					min = mid + 1;
			}
			return -1;
		}

		public static int Recursive<T>(IList<T> items, T key, int min, int max, IComparer<T> comparer) {
			if (min > max)
				return -1;
			else {
				var mid = min + (max - min) / 2;
				var c = comparer.Compare(key, items[mid]);

				if (c == 0)
					return mid;
				else if (c < 0)
					return Recursive(items, key, min, mid - 1, comparer);
				else
					return Recursive(items, key, mid + 1, max, comparer);
			}
		}
	}
}