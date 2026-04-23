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

	[Fact]
	public async Task TestFastPrimes() {
		var values = await FastPrimeGenerator<byte>.GetPrimesAsync().Take(10).ToArrayAsync();
		Assert.Equal([2, 3, 5, 7, 11, 13, 17, 19, 23, 29], values);
	}

	[Fact]
	public async Task ComparePrimes() {
		var v1 = PrimeGenerator<long>.GetPrimes().Take(100000).ToArray();
		var v2 = await FastPrimeGenerator<long>.GetPrimesAsync().Take(100000).ToArrayAsync();
		Assert.Equal(v1, v2);
	}
}