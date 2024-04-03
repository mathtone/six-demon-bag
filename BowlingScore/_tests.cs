namespace BowlingScore {

	public class BowlingScoreTests {

		[Theory]
		[InlineData(10, new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 300)]
		[InlineData(10, new[] { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9 }, 190)]
		[InlineData(10, new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 20)]
		[InlineData(10, new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 1 }, 30)]
		[InlineData(10, new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 1 }, 39)]
		public void ScoreGame(int frames, int[] rolls, int expected) {
			var score = new BowlingScore(frames);
			foreach (var r in rolls) {
				score.Roll(r);
			}
			Assert.True(score.IsComplete);
			Assert.Equal(expected, score.TotalScore);
		}
	}
}