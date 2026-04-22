using System.Numerics;

namespace Six.Demon.Bag.Sequences;

public static class StairCase<T>
	where T : INumber<T> {
	public static IEnumerable<T> GetSequence(int maxJump) {
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxJump);

		var head = 0;
		var rollingSum = T.One;
		var window = new T[maxJump];

		window[0] = T.One;

		while(true) {
			yield return rollingSum;

			var outgoing = window[head];
			window[head] = rollingSum;
			head = (head + 1) % maxJump;

			try {
				rollingSum = checked(rollingSum + (rollingSum - outgoing));
			}
			catch(OverflowException) {
				yield break;
			}
		}
	}

	public static T CountWays(int stairs, int maxJump) {
		ArgumentOutOfRangeException.ThrowIfNegative(stairs);
		//ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxJump);
		return GetSequence(maxJump).ElementAt(stairs);
	}
}