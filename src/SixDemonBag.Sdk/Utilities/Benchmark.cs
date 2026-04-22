using System.Diagnostics;

namespace Six.Demon.Bag.Utilities;

public static class Benchmark {
	public static (TimeSpan Time, T Result) TimedResult<T>(Func<T> func) {
		var sw = Stopwatch.StartNew();
		sw.Restart();
		var result = func();
		sw.Stop();
		return (sw.Elapsed, result);
	}

	public static async Task<(TimeSpan Time, T Result)> AsyncTimedResult<T>(Func<Task<T>> func) {
		var sw = Stopwatch.StartNew();
		sw.Restart();
		var result = await func();
		sw.Stop();
		return (sw.Elapsed, result);
	}
}