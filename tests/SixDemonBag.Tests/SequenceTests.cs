using System.Numerics;

namespace SixDemonBag.Tests;

public class SequenceTests {
	[Fact]
	public void PrimeSequenceStartsWithExpectedValues() =>
		Assert.Equal(
			new BigInteger[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 },
			Primes.Sequence.Take(10));

	[Fact]
	public void FibonacciSequenceStartsWithExpectedValues() =>
		Assert.Equal(
			new BigInteger[] { 0, 1, 1, 2, 3, 5, 8 },
			Fibonacci.Sequence.Take(7));

	[Fact]
	public void FibonacciSupportsLargeValues() =>
		Assert.Equal(
			BigInteger.Parse(
				"434665576869374564356885276750406258025646605173717804024817290895365554179490518904038798400792551692959" +
				"22593080322634775209689623239873322471161642996440906533187938298969649928516003704476137795166849228875"),
			Fibonacci.Sequence.ElementAt(1000));

	[Theory]
	[InlineData(0, 2, 1)]
	[InlineData(1, 2, 1)]
	[InlineData(2, 2, 2)]
	[InlineData(4, 3, 7)]
	public void StairWalkCountsWays(int height, int maxSteps, int expected) =>
		Assert.Equal(new BigInteger(expected), StairWalk.Count(height, maxSteps));

	[Theory]
	[InlineData(-1, 2)]
	[InlineData(1, 0)]
	public void StairWalkRejectsInvalidArguments(int height, int maxSteps) =>
		Assert.Throws<ArgumentOutOfRangeException>(() => StairWalk.Count(height, maxSteps));

	[Fact]
	public void RecursiveFibonacciRejectsNegativeIndex() =>
		Assert.Throws<ArgumentOutOfRangeException>(() => Fibonacci.Recursive(-1));

	[Fact]
	public void BernoulliSequenceStartsWithExpectedValues() =>
		Assert.Equal(
			new[] { "1", "-1/2", "1/6", "0", "-1/30", "0", "1/42" },
			BernoulliNumbers.Sequence.Take(7).Select(value => value.ToString()));
}