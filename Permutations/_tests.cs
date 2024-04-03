using BigMath;

namespace Permutations {
	public class PermutationsTests {

		[Theory]
		[InlineData(45, 10, 2)]
		[InlineData(1326, 52, 2)]
		[InlineData(2598960, 52, 5)]
		public void Calculate_BinomialCoefficient(int expected, int n, int k) =>
			Assert.Equal(expected, BigMaths.BinomialCoefficient(n, k));

		[Fact]
		public void Permutations_OfString() =>
			Assert.Equal(
				new[] { "ABC", "ACB", "BAC", "BCA", "CBA", "CAB" },
				Permutations.GetAllPermutations("ABC")
			);

		[Fact]
		public void Permutations_OfArray() =>
			Assert.Equal(
				new[] {
					new[] {1,2,3 },
					[1,3,2],
					[2,1,3],
					[2,3,1],
					[3,2,1],
					[3,1,2]
				},
				Permutations.GetAllPermutations(1, 2, 3)
			);

		[Fact]
		public void Permutations_GetAllSubsets() =>
			Assert.Equal(1326, Permutations.GetAllSubsets(Enumerable.Range(0, 52), 2).Count());
	}

	public class Permutations {

		public static IEnumerable<string> GetAllPermutations(string input) =>
			GetAllPermutations(input.ToCharArray(), 0).Select(a => new string(a.ToArray()));

		public static IEnumerable<T[]> GetAllPermutations<T>(params T[] items) =>
			GetAllPermutations(items, 0);

		public static IEnumerable<T[]> GetAllPermutations<T>(IEnumerable<T> input) =>
			GetAllPermutations(input, 0);

		protected static IEnumerable<T[]> GetAllPermutations<T>(IEnumerable<T> input, int start = 0) {

			var s = start + 1;
			var list = input.ToArray();

			if (s == list.Length)
				yield return list;
			else {
				foreach (var p in GetAllPermutations(list, s))
					yield return p;

				for (var i = s; i < list.Length; i++) {
					list.Swap(start, i);

					foreach (var v in GetAllPermutations(list, s))
						yield return v.ToArray();

					list.Swap(start, i);
				}
			}
		}

		public static IEnumerable<T[]> GetAllSubsets<T>(IEnumerable<T> items, int choose) =>
			GetAllSubsets(items.ToList(), choose, 0);

		public static IEnumerable<T[]> GetAllSubsets<T>(List<T> items, int choose, int startIndex) {
			if (choose == 0) {
				yield return Array.Empty<T>();
			}
			else {
				for (int i = startIndex; i <= items.Count - choose; i++) {
					foreach (var subset in GetAllSubsets(items, choose - 1, i + 1)) {
						yield return subset.Prepend(items[i]).ToArray();
					}
				}
			}
		}
	}

	public static class CollectionExtensions {
		public static void Swap<T>(this IList<T> items, int a, int b) {
			var t = items[a];
			items[a] = items[b];
			items[b] = t;
		}

		public static void UnSort<T>(this IList<T> items) {
			var rng = new Random();
			for (var i = items.Count; i > 1; i--) {
				items.Swap(rng.Next(i), i - 1);
			}
		}

		public static IEnumerable<T> Randomize<T>(this IList<T> items) {

			var indices = Enumerable.Range(0, items.Count).ToArray();
			indices.UnSort();
			return indices.Select(i => items[i]);
		}
	}
}