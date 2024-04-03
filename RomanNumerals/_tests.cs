namespace RomanNumerals {

	public class RomanNumeralsTests {

		[Theory]
		[InlineData("LXIV", 64)]
		[InlineData("CCXXVI", 226)]
		[InlineData("CM", 900)]
		[InlineData("CMXCVIII", 998)]
		[InlineData("MDCCXII", 1712)]
		[InlineData("M@D$%CC^XII", 1712)]
		public void Convert_ToInteger(string input, int expected) =>
			Assert.Equal(expected, RomanNumeralConverter.ToInteger(input));
	}

	public class RomanNumeralConverter {

		static readonly Dictionary<char, int> numeralToInt = new() {
			{ 'I', 1 },
			{ 'V', 5 },
			{ 'X', 10 },
			{ 'L', 50 },
			{ 'C', 100 },
			{ 'D', 500 },
			{ 'M', 1000 }
		};

		public static int GetValue(char character) {
			numeralToInt.TryGetValue(character, out var rtn);
			return rtn;
		}

		public static int ToInteger(string numeral) {

			var rtn = 0;
			for (var i = 0; i < numeral.Length; i++) {

				var cur = GetValue(numeral[i]);
				var next = i < numeral.Length - 1 ?
					GetValue(numeral[i + 1]) :
					0;

				if (cur < next) {
					cur = next - cur;
					i++;
				}

				rtn += cur;
			}
			return rtn;
		}
	}
}