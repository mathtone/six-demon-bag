
using System.Numerics;
namespace Six.Demon.Bag;

public static class Extensions {
	public static BigInteger Sum(this BigInteger[] arr) {
		var sum = BigInteger.Zero;
		foreach(var item in arr)
			sum += item;
		return sum;
	}
}