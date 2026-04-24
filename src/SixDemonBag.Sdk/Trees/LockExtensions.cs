namespace Six.Demon.Bag.Trees;

public static class LockExtensions {
	public static void Locked(this Lock sync, Action action) {
		lock(sync)
			action();
	}

	public static TResult Locked<TResult>(this Lock sync, Func<TResult> func) {
		lock(sync)
			return func();
	}
}
