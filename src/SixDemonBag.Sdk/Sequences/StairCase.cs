using System.Numerics;

namespace Six.Demon.Bag.Sequences;

public static class StairCase<T>
	where T : IBinaryInteger<T> {
	public static IEnumerable<T> GetSequence(int maxJump) {
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxJump);

		var head = 0;
		var rollingSum = T.One;
		var window = new T[maxJump];
		window[0] = T.One;

		while(true) {
			yield return rollingSum;

			var incoming = rollingSum;
			var outgoing = window[head];
			window[head] = incoming;
			head = (head + 1) % maxJump;

			try {
				rollingSum = checked(rollingSum + incoming - outgoing);
			}
			catch(OverflowException) {
				yield break;
			}
		}
	}

	public static T CountWays(int stairs, int maxJump) {
		ArgumentOutOfRangeException.ThrowIfNegative(stairs);
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxJump);

		var head = 0;
		var rollingSum = T.One;
		var window = new T[maxJump];
		window[0] = T.One;

		for(var i = 0; i < stairs; i++) {
			var incoming = rollingSum;
			var outgoing = window[head];
			window[head] = incoming;
			head = (head + 1) % maxJump;

			rollingSum = checked(rollingSum + incoming - outgoing);
		}

		return rollingSum;
	}
}