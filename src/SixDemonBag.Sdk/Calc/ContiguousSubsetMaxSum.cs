using System.Numerics;

namespace Six.Demon.Bag.Calc;

public static partial class Calculate {

	public static T ContiguousSubsetMaxSum<T>(IList<T> input)
		where T : INumber<T> {
		ArgumentNullException.ThrowIfNull(input);
		
		if(input.Count == 0)
			throw new ArgumentException("Input must contain at least one element.", nameof(input));

		var best = input[0];
		var cur = input[0];

		for(var i = 1; i < input.Count; i++) {
			cur = T.Max(input[i], checked(input[i] + cur));
			best = T.Max(best, cur);
		}

		return best;
	}
}
