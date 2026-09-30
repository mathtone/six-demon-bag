namespace SixDemonBag;

public sealed class BowlingGame {
	private readonly List<int> rolls = [];
	private readonly List<int> frameRolls = [];
	private int frame;

	public bool IsComplete { get; private set; }

	public int Score {
		get {
			if(!IsComplete)
				throw new InvalidOperationException("The game is not complete.");

			var score = 0;
			var roll = 0;

			for(var currentFrame = 0; currentFrame < 10 && roll < rolls.Count; currentFrame++) {
				if(rolls[roll] == 10) {
					score += 10 + GetRoll(roll + 1) + GetRoll(roll + 2);
					roll++;
				}
				else {
					var frameScore = rolls[roll] + GetRoll(roll + 1);
					score += frameScore == 10
						? 10 + GetRoll(roll + 2)
						: frameScore;
					roll += 2;
				}
			}

			return score;
		}
	}

	public void Roll(int pins) {
		if(IsComplete)
			throw new InvalidOperationException("The game is complete.");
		if(pins is < 0 or > 10)
			throw new ArgumentOutOfRangeException(nameof(pins), "Pins must be between 0 and 10.");
		if(pins > PinsRemaining())
			throw new ArgumentOutOfRangeException(nameof(pins), "A roll cannot knock down more than ten pins.");

		rolls.Add(pins);
		frameRolls.Add(pins);

		if(frame < 9) {
			if(pins == 10 || frameRolls.Count == 2)
				CompleteFrame();
			return;
		}

		var strike = frameRolls[0] == 10;
		var spare = frameRolls.Count >= 2 && frameRolls[0] + frameRolls[1] == 10;
		IsComplete = (!strike && !spare && frameRolls.Count == 2) ||
					 ((strike || spare) && frameRolls.Count == 3);
	}

	private int GetRoll(int index) => rolls[index];

	private int PinsRemaining() {
		if(frameRolls.Count == 0)
			return 10;

		if(frame < 9)
			return 10 - frameRolls[0];

		if(frameRolls.Count == 1)
			return frameRolls[0] == 10 ? 10 : 10 - frameRolls[0];

		return frameRolls[0] == 10 && frameRolls[1] < 10
			? 10 - frameRolls[1]
			: 10;
	}

	private void CompleteFrame() {
		frame++;
		frameRolls.Clear();
	}
}