namespace SixDemonBag.Tests;

public class SearchAndSortTests {
	public static TheoryData<int[], int[]> SortCases => new()
	{
		{ [], [] },
		{ [1], [1] },
		{ [2, 1], [1, 2] },
		{ [3, 1, 2, 1], [1, 1, 2, 3] },
		{ [1, 2, 3], [1, 2, 3] },
		{ [3, 2, 1], [1, 2, 3] }
	};

	[Theory]
	[MemberData(nameof(SortCases))]
	public void BubbleSortOrdersValues(int[] values, int[] expected) {
		Sorting.Bubble(values);
		Assert.Equal(expected, values);
	}

	[Theory]
	[MemberData(nameof(SortCases))]
	public void QuickSortOrdersValues(int[] values, int[] expected) {
		Sorting.Quick(values);
		Assert.Equal(expected, values);
	}

	[Theory]
	[MemberData(nameof(SortCases))]
	public void HeapSortOrdersValues(int[] values, int[] expected) {
		Sorting.Heap(values);
		Assert.Equal(expected, values);
	}

	[Fact]
	public void SortsUseCustomComparers() {
		var values = new[] { 1, 3, 2 };
		Sorting.Quick(values, Comparer<int>.Create((left, right) => right.CompareTo(left)));
		Assert.Equal(new[] { 3, 2, 1 }, values);
	}

	[Theory]
	[InlineData(1, 0)]
	[InlineData(11, 5)]
	[InlineData(19, 9)]
	[InlineData(2, -1)]
	public void BinarySearchFindsExpectedIndex(int value, int expected) {
		int[] values = [1, 3, 5, 7, 9, 11, 13, 15, 17, 19];

		Assert.Equal(expected, BinarySearch.Iterative(values, value));
		Assert.Equal(expected, BinarySearch.Recursive(values, value));
	}

	[Fact]
	public void BinarySearchHandlesEmptyInput() {
		Assert.Equal(-1, BinarySearch.Iterative(Array.Empty<int>(), 1));
		Assert.Equal(-1, BinarySearch.Recursive(Array.Empty<int>(), 1));
	}
}