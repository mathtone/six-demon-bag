using System.Net.Quic;
using System.Numerics;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Six.Demon.Bag.Sequences;
class Benchmark1 {

	//WriteCurThreadId("Main");
	//var list = new List<string>();
	//var i = 0;

	//await Task.WhenAll(Enumerable.Range(1, 8).Select(i => DoSomethingAsync()));


	//async Task DoSomethingAsync() {
	//	var id = i++;
	//	var t = new List<int>() {CurrentThread() };
	//	await Task.Yield();
	//	t.Add(CurrentThread());
	//	await Task.Delay(Random.Shared.Next(100, 1000));
	//	t.Add(CurrentThread());
	//	await Task.Delay(Random.Shared.Next(100, 1000));
	//	t.Add(CurrentThread());
	//	list.Add($"{id}-{string.Join(", ", t)}");
	//}

	//foreach(var item in list)
	//	Console.WriteLine(item);
	//static int CurrentThread()=> Environment.CurrentManagedThreadId;
	//static void WriteCurThreadId(string label) => Console.WriteLine($"{label} ThreadId: {Environment.CurrentManagedThreadId}");


	////Console.WriteLine("Different");
	////BenchmarkRunner.Run<MyBenchmarkDemo>();
	//;
	//public class MyBenchmarkDemo {

	//	[Params(10, 20)]
	//	public int N;

	//	[GlobalSetup]
	//	public void GlobalSetup() {
	//		_ = Fibonacci.Sequence32.ToArray();
	//	}

	//	[Benchmark]
	//	public BigInteger[] FibonacciTest1() => Generate(N);

	//	[Benchmark]
	//	public BigInteger[] FibonacciTest2() => Generate<BigInteger>(N);

	//	[Benchmark]
	//	public int[] FibonacciTest3() => Generate<int>(N);

	//	[Benchmark]
	//	public int[] FibonacciTest4() => [.. Fibonacci.Sequence32.Take(N)];

	//	public static BigInteger[] Generate(int sequenceLength) {
	//		var fibonacci = new BigInteger[sequenceLength];
	//		fibonacci[0] = 0;
	//		fibonacci[1] = 1;
	//		for(int index = 2; index < sequenceLength; index++) {
	//			fibonacci[index] = fibonacci[index - 2] + fibonacci[index - 1];
	//		}
	//		return fibonacci;
	//	}

	//	public static T[] Generate<T>(int sequenceLength)
	//		where T : IBinaryInteger<T> {

	//		var rslt = new T[sequenceLength];
	//		rslt[0] = T.Zero;
	//		rslt[1] = T.One;
	//		for(var index = 2; index < sequenceLength; index++) {
	//			rslt[index] = rslt[index - 2] + rslt[index - 1];
	//		}
	//		return rslt;
	//	}

	//	//public static IEnumerable<T> GetSequence<T>(int maxJump) where T : IBinaryInteger<T> {

	//	//	var head = 0;
	//	//	var rollingSum = T.One;
	//	//	var window = new T[maxJump];
	//	//	window[0] = T.One;

	//	//	while(true) {
	//	//		yield return rollingSum;

	//	//		var incoming = rollingSum;
	//	//		var outgoing = window[head];
	//	//		window[head] = incoming;
	//	//		head = (head + 1) % maxJump;

	//	//		try {
	//	//			rollingSum = rollingSum + incoming - outgoing;
	//	//		}
	//	//		catch(OverflowException) {
	//	//			yield break;
	//	//		}
	//	//	}
	//	//}




	//	public static T[] GetResultSet<T>(int stairs, int maxJump) where T : IBinaryInteger<T> {

	//		var result = new T[stairs];
	//		var head = 0;
	//		var rollingSum = T.One;
	//		var window = new T[maxJump];
	//		window[0] = T.One;
	//		for(var i = 0; i < stairs; i++) {
	//			result[i] = rollingSum;
	//			var incoming = rollingSum;
	//			var outgoing = window[head];
	//			window[head] = incoming;
	//			head = (head + 1) % maxJump;
	//			rollingSum = rollingSum + incoming - outgoing;
	//		}
	//		return result;
	//	}
	//}
}