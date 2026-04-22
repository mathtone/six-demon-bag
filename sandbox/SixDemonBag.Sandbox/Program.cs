using System.Collections;


//Console.WriteLine();

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



