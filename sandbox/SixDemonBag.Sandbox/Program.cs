using System.Collections;

using Six.Demon.Bag.Lists;
using Six.Demon.Bag.Primes;
using Six.Demon.Bag.Utilities;

var l = new List<long>();
;
for(var n = 0; n < Array.MaxLength;n++) {
	l.Add(n);
}
l.Add(1);
var x = 100;
var i = x/int.MaxValue;
;

//var x = 5000000;
//Run("PG1 (Warmup)", () => PrimeGenerator<long>.GetPrimes().ElementAt(x));
//await RunAsync("PG2 (Warmup)", async () => await FastPrimeGenerator<long>.GetPrimesAsync(degreeOfParallelism: 1).ElementAtAsync(x));
//Run("PG1 (Control)", () => PrimeGenerator<long>.GetPrimes().ElementAt(x));
//Console.WriteLine();
//for(var i = 16; i <=20; i++) {
//	var seg = (int)Math.Pow(2, i);
//	await RunAsync($"PG2 (1) {seg}", async () => await FastPrimeGenerator<long>.GetPrimesAsync(degreeOfParallelism: 1,segmentOddCount:seg).ElementAtAsync(x));
//	await RunAsync($"PG2 (2) {seg}", async () => await FastPrimeGenerator<long>.GetPrimesAsync(degreeOfParallelism: 2,segmentOddCount:seg).ElementAtAsync(x));
//	await RunAsync($"PG2 (4) {seg}", async () => await FastPrimeGenerator<long>.GetPrimesAsync(degreeOfParallelism: 4,segmentOddCount:seg).ElementAtAsync(x));
//	await RunAsync($"PG2 (8) {seg}", async () => await FastPrimeGenerator<long>.GetPrimesAsync(degreeOfParallelism: 8,segmentOddCount:seg).ElementAtAsync(x));
//	await RunAsync($"PG2 (16) {seg}", async () => await FastPrimeGenerator<long>.GetPrimesAsync(degreeOfParallelism: 16,segmentOddCount:seg).ElementAtAsync(x));
//}
////await RunAsync("PG2", async () => await FastPrimeGenerator<long>.GetPrimesAsync().ElementAtAsync(500000));
////await RunAsync("PG2", async () => await FastPrimeGenerator<int>.GetPrimesAsync().ElementAtAsync(7000));

//static (TimeSpan time, T result) Run<T>(string name, Func<T> action) {
//	var rtn = Benchmark.TimedResult(() => action());
//	Console.WriteLine($"{name} Got {rtn.Result} in {rtn.Time.TotalNanoseconds}ns ({rtn.Time.TotalMilliseconds}ms)");
//	return rtn;
//}

//static async Task<(TimeSpan time, T result)> RunAsync<T>(string name, Func<Task<T>> action) {
//	var rtn = await Benchmark.AsyncTimedResult(async () => await action());
//	Console.WriteLine($"{name} Got {rtn.Result} in {rtn.Time.TotalNanoseconds}ns ({rtn.Time.TotalMilliseconds}ms)");
//	return rtn;
//}

//var d = new Dictionary<long, Guid>();
//var l1 = new LargeList<byte>();
//var bytes = new byte[100000000];
//var i = 0L;
////Random.Shared.NextBytes(bytes);
//while(i < (long)int.MaxValue) {
//	foreach(var b in bytes)
//		d.Add(i++,Guid.NewGuid());
//	Console.WriteLine(i);
//}

//while(l1.Count < (long)int.MaxValue * 2) {
//	foreach(var b in bytes)
//		l1.Add(b);

//	Console.WriteLine(l1.Count);
//}

//Console.WriteLine(l1.Count);

//const int findNumber = 46;
//const int runs = 4;
//for(var i = 0; i <= 2; i++) {

//	Console.WriteLine("Warmup...");
//	Run("F1", () => Fibonacci.RecursiveCalc(findNumber));
//	Run("F2", () => Fibonacci.GetNthNumber<int>(findNumber));





//	Console.WriteLine();
//	var rslt = new {
//		R1 = Enumerable
//			.Range(0, runs)
//			.Select(i => Run("F1", () => Fibonacci.RecursiveCalc(findNumber)))
//			.Average(x => x.time.TotalMilliseconds),
//		R2 = Enumerable
//			.Range(0, runs)
//			.Select(i => Run("F2", () => Fibonacci.GetNthNumber<int>(findNumber)))
//			.Average(x => x.time.TotalMilliseconds)
//	};


//	Console.WriteLine($"\nAverages over {runs} runs:");
//	Console.WriteLine($"F1 Average: {rslt.R1}ms");
//	Console.WriteLine($"F2 Average: {rslt.R2}ms");
//	Console.WriteLine($"\nAdvantage: {Math.Round(rslt.R1 / rslt.R2 * 100, 0):N0}%");
//}


//static (TimeSpan time, T result) Run<T>(string name, Func<T> action) {
//	var rtn = Benchmark.TimedResult(() => action());
//	Console.WriteLine($"{name} Got {rtn.Result} in {rtn.Time.TotalNanoseconds}ns ({rtn.Time.TotalMilliseconds}ms)");
//	return rtn;
//}

//const int findNumber = 184;
//const int runs = 4;

//_ = Fibonacci.GetNthNumber<int>(10);
//_ = Fibonacci.RecursiveCalc(10);
////_ = Fibonacci.Sequence32[46];
////Run("Warmup F1", () => Fibonacci.RecursiveCalc(findNumber));
////Run("Warmup F2", () => Fibonacci.GetNthNumber<int>(findNumber));
////Run("Warmup F3", () => Fibonacci.Sequence32[^1]);

//var rslt = Enumerable
//	.Range(0, runs)
//	.Select(i => new {
//		//F1 = Run("F1", () => Fibonacci.RecursiveCalc(findNumber)),
//		F2 = Run("F2", () => Fibonacci.GetNthNumber<long>(findNumber)),
//		//F3 = Run("F3", () => Fibonacci.Sequence32[^1]),
//	})
//	.ToArray();

////var avgF1 = rslt.Average(x => x.F1.time.TotalMilliseconds);
//var avgF2 = rslt.Average(x => x.F2.time.TotalMilliseconds);
////var avgF3 = rslt.Average(x => x.F3.time.TotalMilliseconds);



