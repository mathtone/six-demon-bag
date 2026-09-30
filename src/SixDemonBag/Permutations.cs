namespace SixDemonBag;

public static class Permutations {
	public static IEnumerable<string> Of(string value) {
		ArgumentNullException.ThrowIfNull(value);
		return Of(value.ToCharArray()).Select(chars => new string(chars));
	}

	public static IEnumerable<T[]> Of<T>(IEnumerable<T> values) {
		ArgumentNullException.ThrowIfNull(values);
		var items = values.ToArray();
		return Generate(0);

		IEnumerable<T[]> Generate(int start) {
			if(start == items.Length) {
				yield return [.. items];
				yield break;
			}

			for(var i = start; i < items.Length; i++) {
				(items[start], items[i]) = (items[i], items[start]);
				foreach(var permutation in Generate(start + 1))
					yield return permutation;
				(items[start], items[i]) = (items[i], items[start]);
			}
		}
	}

	public static IEnumerable<T[]> Subsets<T>(IEnumerable<T> values, int size) {
		ArgumentNullException.ThrowIfNull(values);
		var items = values.ToArray();
		if(size < 0 || size > items.Length)
			throw new ArgumentOutOfRangeException(nameof(size));

		return Generate(0, size);

		IEnumerable<T[]> Generate(int start, int remaining) {
			if(remaining == 0) {
				yield return [];
				yield break;
			}

			for(var i = start; i <= items.Length - remaining; i++)
				foreach(var subset in Generate(i + 1, remaining - 1))
					yield return [items[i], .. subset];
		}
	}
}