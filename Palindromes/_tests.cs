namespace Palindromes {
	public class Palindromes {

		[Theory]
		[InlineData("abcd", "dcbabcd")]
		public void TestMethod(string input, string result) =>
			Assert.Equal(ShortestPalindrome(input), result);

		public static string ShortestPalindrome(string s) {
			var i = 0;
			var j = s.Length - 1;

			while (j >= 0) {
				if (s[i] == s[j]) {
					i++;
				}
				j--;
			}

			if (i == s.Length)
				return s;

			var sfx = s[i..];
			var pfx = new string(sfx.ToCharArray().Reverse().ToArray());
			var mid = ShortestPalindrome(s[..i]);
			return pfx + mid + sfx;
		}
	}
}