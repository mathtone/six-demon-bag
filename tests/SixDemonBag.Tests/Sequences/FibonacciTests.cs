using System.Numerics;

using Six.Demon.Bag.Sequences;

namespace Six.Demon.Bag.Tests.Sequences;

public class FibonacciTests {
	[Fact]
	public void TestFibonacci8_Count() => Assert.Equal(14, Fibonacci.Sequence8.Count);

	[Fact]
	public void TestFibonacci16_Count() => Assert.Equal(24, Fibonacci.Sequence16.Count);

	[Fact]
	public void TestFibonacci32_Count() => Assert.Equal(47, Fibonacci.Sequence32.Count);

	[Fact]
	public void TestFibonacci64_Count() => Assert.Equal(93, Fibonacci.Sequence64.Count);

	[Fact]
	public void TestFibonacci128_Count() => Assert.Equal(185, Fibonacci.Sequence128.Count);

	[Fact]
	public void TestFibonacci32_LastValue() => Assert.Equal(1836311903, Fibonacci.Sequence32[^1]);

	[Fact]
	public void TestFibonacci64_LastValue() => Assert.Equal(7540113804746346429L, Fibonacci.Sequence64[^1]);

	[Fact]
	public void TestFibonacci32_IsIncreasing() {
		var seq = Fibonacci.Sequence32.ToArray();
		Assert.True(seq.Zip(seq.Skip(1), (a, b) => b >= a).All(x => x));
	}
	[Theory]
	[InlineData(0, "0")]
	[InlineData(1, "1")]
	[InlineData(2, "1")]
	[InlineData(3, "2")]
	[InlineData(185, "205697230343233228174223751303346572685")]
	public void TestFibonacciBigInteger(int position, string value) =>
		Assert.Equal(BigInteger.Parse(value), Fibonacci.Sequence.ElementAt(position));

}