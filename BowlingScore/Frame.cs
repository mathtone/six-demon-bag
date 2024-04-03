namespace BowlingScore {
	public class Frame(int number) {

		public int Number { get; set; } = number;
		public List<int> Rolls { get; set; } = new List<int>();
		public int RollsToAdd { get; set; }
		public int ScoreModifier { get; set; }

		public bool IsStrike => Rolls[0] == 10;
		public bool IsSpare => Rolls.Count == 2 && Rolls.Sum() == 10;

		public int? FrameScore => RollsToAdd > 0 ?
			default :
			Rolls.Sum() + ScoreModifier;
	}
}