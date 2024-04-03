namespace BowlingScore {
	public class BowlingScore(int frames = 10) {

		protected int CurrentFrameIdx { get; set; } = 0;
		public IList<Frame> Frames { get; protected set; } = Enumerable
			.Range(1, frames)
			.Select(f => new Frame(f))
			.ToArray();

		public Frame CurrentFrame => Frames[CurrentFrameIdx];
		public bool IsComplete => CurrentFrameIdx == -1;
		public int TotalScore => Frames.Sum(f => f.FrameScore ?? 0);

		public void Roll(int pins) {

			foreach (var f in Frames.Where(f => f.RollsToAdd > 0)) {
				f.ScoreModifier += pins;
				f.RollsToAdd--;
			}

			CurrentFrame.Rolls.Add(pins);
			if (CurrentFrameIdx == Frames.Count - 1) {
				if (CurrentFrame.Rolls[0] == 10 || CurrentFrame.Rolls.Take(2).Sum() == 10) {
					if (CurrentFrame.Rolls.Count == 3)
						CurrentFrameIdx = -1;
				}
				else if (CurrentFrame.Rolls.Count == 2) {
					CurrentFrameIdx = -1;
				}
			}
			else {
				if (CurrentFrame.IsStrike) {
					CurrentFrame.RollsToAdd = 2;
					CurrentFrameIdx++;
				}
				else if (CurrentFrame.IsSpare) {
					CurrentFrame.RollsToAdd = 1;
					CurrentFrameIdx++;
				}
				else if (CurrentFrame.Rolls.Count == 2) {
					CurrentFrameIdx++;
				}
			}
		}
	}
}