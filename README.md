# six-demon-bag

`six-demon-bag` is a collection of .NET 8 algorithm and numeric exercises organized as projects in a single solution.

## Requirements

- .NET SDK 8.0+

## Repository layout

The solution file (`six-demon-bag.sln`) contains top-level projects, including:

- `BigMath` and `BigRationals` for large-number and rational-number helpers
- `Primes`, `BernoulliNumbers`, and `StairWalk` for numeric sequences
- `Sorting`, `BinarySearch`, and `Permutations` for classic algorithms
- `RomanNumerals`, `Palindromes`, `GetMaxProfit`, `BowlingScore`, and `SubsetMaxSum` for kata-style problems
- `SixDemonBag.Rationals` for additional rational number types

## Build and test

```bash
dotnet build six-demon-bag.sln
dotnet test six-demon-bag.sln
```

## Usage examples

```csharp
using BigMath;
using Primes;
using RomanNumerals;

var factorial = BigMaths.Factorial(10);
var binomial = BigMaths.BinomialCoefficient(52, 5);
var firstTenPrimes = PrimeNumbers.Sequence.Take(10).ToArray();
var romanValue = RomanNumeralConverter.ToInteger("CMXCVIII");
```
