using Xunit.Abstractions;

namespace SubsetMaxSum {

	public class SubsetMaxSumTests(ITestOutputHelper output) {
		[Theory, MemberData(nameof(MaxSumCases))]
		public void SubArraySum(int[] input, int expected) {
			var actual = MaxSum(input);
			output.WriteLine("Maximum contiguous sum is " + actual);
			Assert.Equal(expected, actual);
		}

		static int MaxSum(int[] input) {

			var rtn = input[0];
			var cur = input[0];

			for (int i = 1; i < input.Length; i++) {
				cur = Math.Max(input[i], input[i] + cur);
				rtn = Math.Max(rtn, cur);
			}

			return rtn;
		}

		public static IEnumerable<object[]> MaxSumCases => [
			[new[] { 1,2,3,4,5 },15],
			[new[] { 1,2,3,4,5,6 },21],
			[new[] { 30,-99,3,4,25,-1 },32],
			[new[] { 30,-99,100,-101,25,-1 },100],
			[new[] { 30,-99,100,-101,25,-1,100 },124]
		];
	}
}