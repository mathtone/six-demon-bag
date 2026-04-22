using Six.Demon.Bag.Sequences;

namespace Six.Demon.Bag.Tests.Sequences;

public class StairWalkTests {

	[Fact]
	public async Task TestStairWalk3_FirstTerms() {
		var values = StairCase<int>.GetSequence(3).Take(8);
		Assert.Equal([1, 1, 2, 4, 7, 13, 24, 44], values);
	}

	[Fact]
	public async Task TestStairWalk1_AllOnes() {
		var values = StairCase<byte>.GetSequence(1).Take(5);
		Assert.Equal([1, 1, 1, 1, 1], values);
	}

	[Fact]
	public async Task TestStairWalk_InvalidJump() =>
		Assert.Throws<ArgumentOutOfRangeException>(() => StairCase<int>.GetSequence(0).GetEnumerator().MoveNext());

}
