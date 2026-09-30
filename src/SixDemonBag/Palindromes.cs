namespace SixDemonBag;

public static class Palindromes {
	public static string ShortestByPrepending(string value) {
		ArgumentNullException.ThrowIfNull(value);

		var matchingPrefix = 0;
		for(var end = value.Length - 1; end >= 0; end--)
			if(value[matchingPrefix] == value[end])
				matchingPrefix++;

		if(matchingPrefix == value.Length)
			return value;

		var suffix = value[matchingPrefix..];
		return string.Concat(suffix.Reverse()) +
			   ShortestByPrepending(value[..matchingPrefix]) +
			   suffix;
	}
}