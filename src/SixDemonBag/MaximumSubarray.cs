namespace SixDemonBag;

public static class MaximumSubarray {
	public static int Sum(IReadOnlyList<int> values) {
		ArgumentNullException.ThrowIfNull(values);
		if(values.Count == 0)
			throw new ArgumentException("At least one value is required.", nameof(values));

		var best = values[0];
		var current = values[0];

		for(var i = 1; i < values.Count; i++) {
			current = Math.Max(values[i], checked(current + values[i]));
			best = Math.Max(best, current);
		}

		return best;
	}
}