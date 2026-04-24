using System.Diagnostics;

namespace Six.Demon.Bag.Utilities;

public static class Timing {

	public static async Task<TimeSpan> Timed(this Task task) {
		var sw = Stopwatch.StartNew();
		await task.ConfigureAwait(false);
		sw.Stop();
		return sw.Elapsed;
	}

	public static async Task<(TimeSpan, RSLT)> Timed<RSLT>(this Task<RSLT> task) {
		var sw = Stopwatch.StartNew();
		var result = await task.ConfigureAwait(false);
		sw.Stop();
		return (sw.Elapsed, result);
	}

	public static async Task<(TimeSpan, RSLT)> Timed<RSLT>(this ValueTask<RSLT> task) {
		var sw = Stopwatch.StartNew();
		var result = await task.ConfigureAwait(false);
		sw.Stop();
		return (sw.Elapsed, result);
	}

	public static async Task<TimeSpan> Timed(this Action action) {
		var sw = Stopwatch.StartNew();
		action();
		sw.Stop();
		return (sw.Elapsed);
	}

	public static async Task<(TimeSpan, RSLT)> Timed<RSLT>(this Func<RSLT> func) {
		var sw = Stopwatch.StartNew();
		var result = func();
		sw.Stop();
		return (sw.Elapsed, result);
	}
}