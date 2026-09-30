namespace SixDemonBag.Tests;

public class RomanNumeralTests {
	[Theory]
	[InlineData("I", 1)]
	[InlineData("IV", 4)]
	[InlineData("LXIV", 64)]
	[InlineData("CMXCVIII", 998)]
	[InlineData("MDCCXII", 1712)]
	[InlineData("MMMCMXCIX", 3999)]
	public void CanonicalNumeralsRoundTrip(string numeral, int value) {
		Assert.Equal(value, RomanNumerals.Parse(numeral));
		Assert.Equal(numeral, RomanNumerals.Format(value));
	}

	[Theory]
	[InlineData("IIII")]
	[InlineData("VX")]
	[InlineData("iv")]
	[InlineData("M@D")]
	public void InvalidNumeralsAreRejected(string numeral) =>
		Assert.Throws<FormatException>(() => RomanNumerals.Parse(numeral));

	[Theory]
	[InlineData(0)]
	[InlineData(4000)]
	public void ValuesOutsideRomanRangeAreRejected(int value) =>
		Assert.Throws<ArgumentOutOfRangeException>(() => RomanNumerals.Format(value));
}