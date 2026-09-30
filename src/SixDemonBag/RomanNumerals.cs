namespace SixDemonBag;

public static class RomanNumerals {
	private static readonly (int Value, string Numeral)[] Tokens =
	[
		(1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
		(100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
		(10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
	];

	public static int Parse(string numeral) {
		ArgumentException.ThrowIfNullOrEmpty(numeral);

		var value = 0;
		var index = 0;

		foreach(var token in Tokens)
			while(numeral.AsSpan(index).StartsWith(token.Numeral)) {
				value += token.Value;
				index += token.Numeral.Length;
			}

		if(index != numeral.Length || value is < 1 or > 3999 || Format(value) != numeral)
			throw new FormatException("Roman numeral must use canonical uppercase notation.");

		return value;
	}

	public static string Format(int value) {
		if(value is < 1 or > 3999)
			throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 1 and 3999.");

		var numeral = new System.Text.StringBuilder();
		foreach(var token in Tokens)
			while(value >= token.Value) {
				numeral.Append(token.Numeral);
				value -= token.Value;
			}

		return numeral.ToString();
	}
}