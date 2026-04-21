using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

using Six.Demon.Bag.Sequences;
var e = 2000000;
//var fpg = new FastPrimeGenerator<int>();

Console.WriteLine("ASYNC 1");
var (time, result) = await AsyncTimedResult(async () => await FastPrimeGenerator<long>.GetPrimesAsync().ElementAtAsync(e));
Console.WriteLine($"Prime #{e}: {result} (calculated cold with 1 threads in {time.TotalMilliseconds} ms)");

for(var c = 4; c <= Environment.ProcessorCount; c+=4) {
	//for(var i = 0; i < 6; i++) {
		(time, result) = await AsyncTimedResult(async () => await FastPrimeGenerator<long>.GetPrimesAsync().ElementAtAsync(e));
		Console.WriteLine($"Prime #{e}: {result} (calculated with {c} threads in {time.TotalMilliseconds} ms)");
	//}
}

static (TimeSpan time, T result) TimedResult<T>(Func<T> func) {
	var sw = Stopwatch.StartNew();
	var result = func();
	sw.Stop();
	return (sw.Elapsed, result);
}

static async Task<(TimeSpan time, T result)> AsyncTimedResult<T>(Func<Task<T>> func) {
	var sw = Stopwatch.StartNew();
	var result = await func();
	sw.Stop();
	return (sw.Elapsed, result);
}