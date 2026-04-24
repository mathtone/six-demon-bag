namespace Six.Demon.Bag.Calc;

public static partial class Calculate {

	public static string ShortestPalindrome(string input) {
		ArgumentNullException.ThrowIfNull(input);
		return new string(ShortestPalindrome(input.ToCharArray()));
	}
	public static T[] ShortestPalindrome<T>(T[] input) =>
		ShortestPalindrome(input, EqualityComparer<T>.Default);

	public static T[] ShortestPalindrome<T>(T[] input, IEqualityComparer<T> comparer) {
		ArgumentNullException.ThrowIfNull(input);

		if(input.Length <= 1)
			return input;

		var i = 0;
		var j = input.Length - 1;

		while(j >= 0) {
			if(comparer.Equals(input[i], input[j]))
				i++;
			j--;
		}

		if(i == input.Length)
			return input;

		var suffix = input[i..];
		return [
			.. suffix.Reverse(),
			.. ShortestPalindrome(input[..i]),
			.. suffix,
		];
	}
	//This is supposed to be faster but it seems to be slower in practice, likely due to the overhead of building the combined array and the LPS array.
	public static T[] ShortestPalindromeKMP<T>(T[] input) {
		ArgumentNullException.ThrowIfNull(input);

		if(input.Length <= 1)
			return input;

		var n = input.Length;
		var comparer = EqualityComparer<T>.Default;
		var combined = new T[n * 2 + 1];
		var sepIndex = n;
		var lps = new int[combined.Length];

		for(var i = 0; i < n; i++)
			combined[i] = input[i];

		for(var i = 0; i < n; i++)
			combined[sepIndex + 1 + i] = input[n - 1 - i];

		for(var i = 1; i < combined.Length; i++) {
			var j = lps[i - 1];

			while(j > 0 && !EqualsWithSeparator(combined, i, j, sepIndex, comparer))
				j = lps[j - 1];

			if(EqualsWithSeparator(combined, i, j, sepIndex, comparer))
				j++;

			lps[i] = j;
		}

		var longestPrefix = lps[^1];
		var suffixLength = n - longestPrefix;
		var result = new T[n + suffixLength];

		// reversed suffix at front
		for(var i = 0; i < suffixLength; i++)
			result[i] = input[n - 1 - i];

		Array.Copy(input, 0, result, suffixLength, n);
		return result;
	}

	private static bool EqualsWithSeparator<T>(T[] arr, int i, int j, int sepIndex, EqualityComparer<T> comparer) =>
		i != sepIndex && j != sepIndex && comparer.Equals(arr[i], arr[j]);

	public static string ShortestPalindromeKMP(string s, char separator = '☺') {
		ArgumentNullException.ThrowIfNull(s);

		if(s.Length <= 1)
			return s;

		//if(s.Contains(separator))
		//	throw new ArgumentException("Separator must not appear in the input.", nameof(separator));

		var rev = new string([.. s.Reverse()]);
		var combined = s + separator + rev;
		var lps = new int[combined.Length];

		for(var i = 1; i < combined.Length; i++) {

			var j = lps[i - 1];

			while(j > 0 && combined[i] != combined[j])
				j = lps[j - 1];

			if(combined[i] == combined[j])
				j++;

			lps[i] = j;
		}

		var longestPrefix = lps[^1];
		var suffix = s[longestPrefix..];
		return new string([.. suffix.Reverse()]) + s;
	}
}