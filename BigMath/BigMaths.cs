using System.Numerics;

namespace BigMath {
	public static class BigMaths {

		public static BigInteger Factorial(int n) {
			var result = (BigInteger)1;
			for (int i = 1; i <= n; i++)
				result *= i;
			return result;
		}

		public static BigInteger BinomialCoefficient(int n, int k) {
			if (n < 0 || k < 0 || n < k)
				throw new ArgumentException("Invalid parameters");

			if (k > n - k)
				k = n - k;
			
			var result = new BigInteger(1);

			for (int i = 1; i <= k; i++) {
				result *= n - (k - i);
				result /= i;
			}

			return result;
		}


		public static int Factorial32(int n) {
			var result = 1;
			for (int i = 1; i <= n; i++)
				result *= i;
			return result;
		}

		public static int BinomialCoefficient32(int n, int k) {
			if (n < 0 || k < 0 || n < k)
				throw new ArgumentException("Invalid parameters");

			if (k > n - k)
				k = n - k;

			var result = 1;

			for (var i = 1; i <= k; i++) {
				result *= n - (k - i);
				result /= i;
			}

			return result;
		}

		public static long Factorial64(long n) {
			var result = 1L;
			for (var i = 1L; i <= n; i++)
				result *= i;
			return result;
		}

		public static long BinomialCoefficient64(int n, int k) {
			if (n < 0 || k < 0 || n < k)
				throw new ArgumentException("Invalid parameters");

			if (k > n - k)
				k = n - k;

			var result = 1L;

			for (var i = 1; i <= k; i++) {
				result *= n - (k - i);
				result /= i;
			}

			return result;
		}
	}
}