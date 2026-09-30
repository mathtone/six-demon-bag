namespace SixDemonBag.Tests;

public class CombinatorialTests {
	[Fact]
	public void StringPermutationsAreGenerated() {
		var permutations = Permutations.Of("ABC").ToArray();

		Assert.Equal(6, permutations.Length);
		Assert.Equal(
			new[] { "ABC", "ACB", "BAC", "BCA", "CBA", "CAB" }.Order(),
			permutations.Order());
	}

	[Fact]
	public void EmptyInputHasOnePermutation() =>
		Assert.Equal([string.Empty], Permutations.Of(string.Empty));

	[Fact]
	public void SubsetsAreGenerated() =>
		Assert.Equal(1326, Permutations.Subsets(Enumerable.Range(0, 52), 2).Count());

	[Fact]
	public void EmptySubsetIsGenerated() =>
		Assert.Equal([Array.Empty<int>()], Permutations.Subsets([1, 2], 0));

	[Theory]
	[InlineData(-1)]
	[InlineData(4)]
	public void InvalidSubsetSizesAreRejected(int size) =>
		Assert.Throws<ArgumentOutOfRangeException>(() => Permutations.Subsets([1, 2, 3], size));

	[Theory]
	[InlineData("", "")]
	[InlineData("a", "a")]
	[InlineData("aba", "aba")]
	[InlineData("abcd", "dcbabcd")]
	[InlineData("aacecaaa", "aaacecaaa")]
	public void ShortestPalindromeIsBuiltByPrepending(string input, string expected) =>
		Assert.Equal(expected, Palindromes.ShortestByPrepending(input));
}