using Six.Demon.Bag.Primes;

namespace Six.Demon.Bag.Tests.Primes;

public class PrimesTests {

	[Fact]
	public async Task TestPrimes() {
		var values = PrimeGenerator<int>.GetPrimes().Take(10);
		Assert.Equal([2, 3, 5, 7, 11, 13, 17, 19, 23, 29], values);
	}

	[Fact]
	public async Task TestPrimes8() {
		var values = PrimeGenerator<byte>.GetPrimes().ToArray();
		Assert.Equal(54, values.Length);
	}
}