namespace SixDemonBag;

public static class MaximumSubarray {
	public static int Sum(IReadOnlyList<int> values) {
		ArgumentNullException.ThrowIfNull(values);
		if(values.Count == 0)
			throw new ArgumentException("At least one value is required.", nameof(values));

		long best = values[0];
		long current = values[0];

		for(var i = 1; i < values.Count; i++) {
			current = Math.Max(values[i], current + values[i]);
			best = Math.Max(best, current);
		}

		return checked((int)best);
	}
}