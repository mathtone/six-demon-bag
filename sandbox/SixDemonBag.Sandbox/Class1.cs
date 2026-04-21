using System.Numerics;
using System.Text;

using Math = System.Math;
namespace Six.Demon.Bag.Sandbox;

public static partial class PiBinary {
	private const long A = 13591409;
	private const long B = 545140134;
	private const long C = 640320;
	private static readonly BigInteger C3_OVER_24 = BigInteger.Pow(new BigInteger(C), 3) / 24;

	private struct PQT {
		public BigInteger P;
		public BigInteger Q;
		public BigInteger T;

		public PQT(BigInteger p, BigInteger q, BigInteger t) {
			P = p;
			Q = q;
			T = t;
		}
	}

	public static BigInteger ComputePiScaledBinary(int fractionalBits) {
		if(fractionalBits < 1)
			throw new ArgumentOutOfRangeException(nameof(fractionalBits), "Must be >= 1.");

		double decimalDigits = fractionalBits * Math.Log10(2.0);
		int terms = (int)(decimalDigits / 14.181647462) + 2;

		PQT result = BinarySplit(0, terms);

		BigInteger sqrtInput = new BigInteger(10005) << (2 * fractionalBits);
		BigInteger sqrtTerm = Sqrt(sqrtInput);

		BigInteger numerator = result.Q * 426880 * sqrtTerm;
		BigInteger piScaled = numerator / result.T;

		return piScaled;
	}

	private static PQT BinarySplit(int a, int b) {
		if(b - a == 1) {
			if(a == 0) {
				return new PQT(BigInteger.One, BigInteger.One, new BigInteger(A));
			}

			BigInteger k = new BigInteger(a);

			BigInteger p = (6 * k - 5) * (2 * k - 1) * (6 * k - 1);
			BigInteger q = k * k * k * C3_OVER_24;
			BigInteger t = p * (A + B * k);

			if((a & 1) == 1)
				t = -t;

			return new PQT(p, q, t);
		}

		int m = (a + b) / 2;

		PQT left = BinarySplit(a, m);
		PQT right = BinarySplit(m, b);

		BigInteger pResult = left.P * right.P;
		BigInteger qResult = left.Q * right.Q;
		BigInteger tResult = left.T * right.Q + left.P * right.T;

		return new PQT(pResult, qResult, tResult);
	}

	private static BigInteger Sqrt(BigInteger n) {
		if(n.Sign < 0)
			throw new ArgumentOutOfRangeException(nameof(n), "Cannot take sqrt of a negative number.");
		if(n.IsZero)
			return BigInteger.Zero;
		if(n == BigInteger.One)
			return BigInteger.One;

		int bitLength = GetBitLength(n);
		BigInteger x = BigInteger.One << ((bitLength + 1) / 2);

		while(true) {
			BigInteger y = (x + n / x) >> 1;
			if(y >= x)
				return x;
			x = y;
		}
	}

	private static int GetBitLength(BigInteger n) {
		if(n.Sign < 0)
			n = BigInteger.Abs(n);
		if(n.IsZero)
			return 0;

		byte[] bytes = n.ToByteArray();
		byte msb = bytes[bytes.Length - 1];

		int bits = (bytes.Length - 1) * 8;
		int highBits = 0;

		while(msb != 0) {
			msb >>= 1;
			highBits++;
		}

		return bits + highBits;
	}

	public static string ToBinaryFixedPoint(BigInteger scaledValue, int fractionalBits) {
		BigInteger integerPart = scaledValue >> fractionalBits;
		BigInteger fractionalMask = (BigInteger.One << fractionalBits) - 1;
		BigInteger fractionalPart = scaledValue & fractionalMask;

		string intBits = ToBinary(integerPart);
		StringBuilder sb = new StringBuilder();
		sb.Append(intBits);
		sb.Append('.');

		for(int i = fractionalBits - 1; i >= 0; i--) {
			BigInteger bit = (fractionalPart >> i) & BigInteger.One;
			sb.Append(bit.IsZero ? '0' : '1');
		}

		return sb.ToString();
	}

	private static string ToBinary(BigInteger value) {
		if(value.IsZero)
			return "0";

		StringBuilder sb = new StringBuilder();

		while(value > 0) {
			sb.Insert(0, (value & BigInteger.One) == BigInteger.One ? '1' : '0');
			value >>= 1;
		}

		return sb.ToString();
	}
}


public static partial class PiBinary {
	/// <summary>
	/// Yields the fractional binary digits of pi from left to right.
	/// Each yielded value is 0 or 1.
	/// </summary>
	public static IEnumerable<int> EnumeratePiFractionBits(int fractionalBits) {
		if(fractionalBits < 0)
			throw new ArgumentOutOfRangeException(nameof(fractionalBits));

		BigInteger piScaled = ComputePiScaledBinary(fractionalBits);
		return EnumerateFractionBits(piScaled, fractionalBits);
	}

	/// <summary>
	/// Yields the fractional bits of a fixed-point value where scaledValue = floor(x * 2^fractionalBits).
	/// Bits are yielded from most significant fractional bit to least significant.
	/// </summary>
	public static IEnumerable<int> EnumerateFractionBits(BigInteger scaledValue, int fractionalBits) {
		if(fractionalBits < 0)
			throw new ArgumentOutOfRangeException(nameof(fractionalBits));

		for(int bitIndex = fractionalBits - 1; bitIndex >= 0; bitIndex--) {
			yield return ((scaledValue >> bitIndex) & BigInteger.One).IsZero ? 0 : 1;
		}
	}

	/// <summary>
	/// Yields the full binary expansion of the fixed-point value:
	/// integer part first, then a separator, then fractional bits.
	/// Useful if you still want something like 11.00101... without building a full string.
	/// </summary>
	public static IEnumerable<char> EnumerateBinaryFixedPoint(BigInteger scaledValue, int fractionalBits) {
		if(fractionalBits < 0)
			throw new ArgumentOutOfRangeException(nameof(fractionalBits));

		BigInteger integerPart = scaledValue >> fractionalBits;

		foreach(char c in ToBinary(integerPart))
			yield return c;

		yield return '.';

		for(int bitIndex = fractionalBits - 1; bitIndex >= 0; bitIndex--) {
			yield return ((scaledValue >> bitIndex) & BigInteger.One).IsZero ? '0' : '1';
		}
	}
}