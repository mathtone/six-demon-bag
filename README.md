# six-demon-bag

`six-demon-bag` is a .NET 10 math/algorithms library with supporting tests and a sandbox app.

## Repository layout

- `src/SixDemonBag.Sdk` — core library
- `tests/SixDemonBag.Tests` — xUnit test project
- `sandbox/SixDemonBag.Sandbox` — executable playground/benchmark area

## Requirements

- .NET SDK `10.0.0` (see `global.json`)

## Build and test

```bash
dotnet build six-demon-bag.sln
dotnet test six-demon-bag.sln
```

## Core modules

### `Six.Demon.Bag.Calc`

- Factorials (`Factorial`, `Factorial32/64/128`)
- Binomial coefficient and GCD
- Kadane-style max contiguous subset sum
- Max stock-trading profit DP
- Shortest-palindrome helpers

### `Six.Demon.Bag.Primes`

- `PrimeGenerator<T>.GetPrimes()` for incremental prime generation
- `FastPrimeGenerator<T>.GetPrimesAsync(...)` for segmented async prime generation

### `Six.Demon.Bag.Sequences`

- `Fibonacci` sequences and nth-value helpers
- `StairCase<T>` rolling-window sequence/counting
- `BernoulliNumbers<T>.Sequence`

### `Six.Demon.Bag.Rationals`

- `Rational<T>` value type with normalization, arithmetic operators, comparisons, and conversion helpers
- `IRational<T>` abstraction

### `Six.Demon.Bag.Lists`

- `LargeList<T>` chunked long-index list implementation
- Segment/view extension helpers

### `Six.Demon.Bag.Trees`

- `Tree<T>` and `ThreadSafeTree<T>` implementations of `ITree<T>`

### `Six.Demon.Bag.Permutations`

- Permutation/subset generation utilities

## Usage examples

```csharp
using System.Linq;
using Six.Demon.Bag.Calc;
using Six.Demon.Bag.Primes;
using Six.Demon.Bag.Rationals;
using Six.Demon.Bag.Sequences;

var n52 = Calculate.Factorial(52);
var choose = Calculate.BinomialCoefficient(52, 5); // 2,598,960

var firstTenPrimes = PrimeGenerator<int>.GetPrimes().Take(10).ToArray();
var fib100 = Fibonacci.GetNthNumber<System.Numerics.BigInteger>(100);

var r = Rational.Create(2, 3);
var sum = r + Rational.Create(1, 6); // 5/6
```

## Notes

- This repository currently targets `net10.0` via shared `Directory.Build.props`.
- The sandbox project references `BenchmarkDotNet` for local experimentation.
