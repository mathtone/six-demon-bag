namespace SixDemonBag.Tests;

public class BowlingGameTests {
	[Theory]
	[InlineData(new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 300)]
	[InlineData(new[] { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9 }, 190)]
	[InlineData(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 20)]
	[InlineData(new[] { 10, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 24)]
	public void CompleteGamesAreScored(int[] rolls, int expected) {
		var game = Play(rolls);

		Assert.True(game.IsComplete);
		Assert.Equal(expected, game.Score);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(11)]
	public void InvalidPinCountsAreRejected(int pins) =>
		Assert.Throws<ArgumentOutOfRangeException>(() => new BowlingGame().Roll(pins));

	[Fact]
	public void FrameCannotExceedTenPins() {
		var game = new BowlingGame();
		game.Roll(8);

		Assert.Throws<ArgumentOutOfRangeException>(() => game.Roll(3));
	}

	[Fact]
	public void TenthFrameFillRollObservesResetPins() {
		var game = Play(Enumerable.Repeat(0, 18).Append(10).Append(7));

		Assert.Throws<ArgumentOutOfRangeException>(() => game.Roll(4));
	}

	[Fact]
	public void RollingAfterCompletionIsRejected() {
		var game = Play(Enumerable.Repeat(0, 20));

		Assert.Throws<InvalidOperationException>(() => game.Roll(0));
	}

	[Fact]
	public void IncompleteGameCannotBeScored() =>
		Assert.Throws<InvalidOperationException>(() => new BowlingGame().Score);

	private static BowlingGame Play(IEnumerable<int> rolls) {
		var game = new BowlingGame();
		foreach(var pins in rolls)
			game.Roll(pins);
		return game;
	}
}