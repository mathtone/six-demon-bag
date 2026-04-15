using System.Numerics;
using Six.Demon.Bag.Calc;

namespace Six.Demon.Bag.Tests.Calc;

public class FactorialTests {

	[Theory]
	[InlineData(0, "1")]
	[InlineData(1, "1")]
	[InlineData(2, "2")]
	[InlineData(10, "3628800")]
	[InlineData(22, "1124000727777607680000")]
	[InlineData(52, "80658175170943878571660636856403766975289505440883277824000000000000")]
	public void TestFactorial(int n, string expected) =>
		Assert.Equal(BigInteger.Parse(expected), Calculate.Factorial(n));
}
