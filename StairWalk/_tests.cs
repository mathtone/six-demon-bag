using System.Collections.Immutable;
using System.Numerics;
using Xunit.Abstractions;

namespace StairWalk {
	public class StairWalkTests {

		private ITestOutputHelper output;

		public StairWalkTests(ITestOutputHelper output) => this.output = output;

		[Fact]
		public void GetFibonacciSequence() =>
			Assert.Equal([0, 1, 1, 2, 3, 5, 8], StairWalk.FibonacciSequence.Take(7));

		[Fact]
		public void GetFibonacciSequence2() =>
			Assert.Equal([0, 1, 1, 2, 3, 5, 8], Fibonacci.Sequence.Take(7));

		[Fact]
		public void GetBigFib() => Assert.Equal(
			BigInteger.Parse(
				"434665576869374564356885276750406258025646605173717804024817290895365554179490518904038798400792551692959" +
				"22593080322634775209689623239873322471161642996440906533187938298969649928516003704476137795166849228875"
			),
			StairWalk.FibonacciSequence.ElementAt(1000)
		);

		[Fact]
		public void HowManyFibInts() {
			var x = 0;
			try {
				foreach (var i in StairWalk.FibonacciSequence.Take(100)) {
					_ = Convert.ToInt32(i.ToString());
					++x;
					//output.WriteLine($"{i},");
				}
			}
			catch (System.OverflowException) { }
			;

			Assert.Equal(47, x);
		}


		[Fact]
		public void HowManyFibLongs() {
			var x = 0L;
			try {
				foreach (var i in StairWalk.FibonacciSequence.Take(100)) {
					_ = Convert.ToInt64(i.ToString());
					++x;
					//output.WriteLine($"{i},");
				}
			}
			catch (System.OverflowException) { }
			;

			Assert.Equal(93L, x);
		}

		[Theory]
		[InlineData(1, 2, 1)]
		[InlineData(2, 2, 2)]
		[InlineData(5, 2, 4)]
		[InlineData(4, 3, 3)]
		[InlineData(7, 3, 4)]
		public void WalkStairs(int expected, int maxSteps, int stairs) => Assert.Equal((BigInteger)expected, StairWalk.GetWalksToTop(stairs, maxSteps));
	}

	public static class Fibonacci {

		public static IEnumerable<BigInteger> Sequence {
			get {

				BigInteger a, b, c;

				yield return a = 0;
				yield return b = 1;

				while (true) {
					c = a + b;
					a = b;
					yield return b = c;
				}
			}
		}

		public static BigInteger RecursiveFibonacci(int n) {
			if (n <= 0) {
				return 0;
			}
			else if (n == 1) {
				return 1;
			}
			else {
				return RecursiveFibonacci(n - 1) + RecursiveFibonacci(n - 2);
			}
		}

		public static readonly ImmutableArray<int> Ints = [
			0,
			1,
			1,
			2,
			3,
			5,
			8,
			13,
			21,
			34,
			55,
			89,
			144,
			233,
			377,
			610,
			987,
			1597,
			2584,
			4181,
			6765,
			10946,
			17711,
			28657,
			46368,
			75025,
			121393,
			196418,
			317811,
			514229,
			832040,
			1346269,
			2178309,
			3524578,
			5702887,
			9227465,
			14930352,
			24157817,
			39088169,
			63245986,
			102334155,
			165580141,
			267914296,
			433494437,
			701408733,
			1134903170,
			1836311903
		];

		public static readonly ImmutableArray<long> Longs = [
			0,
			1,
			1,
			2,
			3,
			5,
			8,
			13,
			21,
			34,
			55,
			89,
			144,
			233,
			377,
			610,
			987,
			1597,
			2584,
			4181,
			6765,
			10946,
			17711,
			28657,
			46368,
			75025,
			121393,
			196418,
			317811,
			514229,
			832040,
			1346269,
			2178309,
			3524578,
			5702887,
			9227465,
			14930352,
			24157817,
			39088169,
			63245986,
			102334155,
			165580141,
			267914296,
			433494437,
			701408733,
			1134903170,
			1836311903,
			2971215073,
			4807526976,
			7778742049,
			12586269025,
			20365011074,
			32951280099,
			53316291173,
			86267571272,
			139583862445,
			225851433717,
			365435296162,
			591286729879,
			956722026041,
			1548008755920,
			2504730781961,
			4052739537881,
			6557470319842,
			10610209857723,
			17167680177565,
			27777890035288,
			44945570212853,
			72723460248141,
			117669030460994,
			190392490709135,
			308061521170129,
			498454011879264,
			806515533049393,
			1304969544928657,
			2111485077978050,
			3416454622906707,
			5527939700884757,
			8944394323791464,
			14472334024676221,
			23416728348467685,
			37889062373143906,
			61305790721611591,
			99194853094755497,
			160500643816367088,
			259695496911122585,
			420196140727489673,
			679891637638612258,
			1100087778366101931,
			1779979416004714189,
			2880067194370816120,
			4660046610375530309,
			7540113804746346429
		];
	}

	public static class StairWalk {

		public static BigInteger GetWalksToTop(int height, int maxSteps) =>
			Walk(maxSteps).ElementAt(height + 1);

		public static IEnumerable<BigInteger> FibonacciSequence =>
			Walk(2);

		static IEnumerable<BigInteger> Walk(int maxSteps) {

			var r = new BigInteger[maxSteps + 1];
			yield return r[0] = 0;
			yield return r[1] = 1;

			for (var i = 2; i < maxSteps; i++)
				yield return r[i] = Sum(r[0..i]);

			while (true) {
				r[maxSteps] = 0;
				for (var i = 0U; i < maxSteps; i++) {
					r[maxSteps] += r[i];
					r[i] = r[i + 1];
				}
				yield return r[maxSteps];
			}
		}

		static BigInteger Sum(IEnumerable<BigInteger> items) {
			var rtn = new BigInteger();
			foreach (var item in items) {
				rtn += item;
			}
			return rtn;
		}
	}
}