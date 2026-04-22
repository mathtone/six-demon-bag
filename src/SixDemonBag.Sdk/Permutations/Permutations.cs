namespace Six.Demon.Bag.Permutations;

public class Permutations {

	public static IEnumerable<string> GetAllPermutations(string input) =>
		GetAllPermutations(input.ToCharArray(), 0).Select(a => new string(a.ToArray()));

	public static IEnumerable<T[]> GetAllPermutations<T>(params T[] items) =>
		GetAllPermutations(items, 0);

	protected static IEnumerable<T[]> GetAllPermutations<T>(IEnumerable<T> input, int start = 0) {

		var s = start + 1;
		var list = input.ToArray();

		if(s == list.Length)
			yield return list;
		else {
			foreach(var p in GetAllPermutations(list, s))
				yield return p;

			for(var i = s; i < list.Length; i++) {
				list.Swap(start, i);

				foreach(var v in GetAllPermutations(list, s))
					yield return v.ToArray();

				list.Swap(start, i);
			}
		}
	}

	public static IEnumerable<T[]> GetAllSubsets<T>(List<T> items, int choose, int startIndex=0) {
		if(choose == 0) {
			yield return [];
		}
		else {
			for(var i = startIndex; i <= items.Count - choose; i++) {
				foreach(var subset in GetAllSubsets(items, choose - 1, i + 1)) {
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

	//public static void Shuffle<T>(this IList<T> items) {
	//	var rng = new Random();
	//	for(var i = items.Count; i > 1; i--) {
	//		items.Swap(rng.Next(i), i - 1);
	//	}
	//}
}