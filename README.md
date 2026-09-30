# Six Demon Bag

Short, idiomatic C# solutions to common interview problems.

## Challenges

| Area | Solutions |
|---|---|
| Search and sorting | Binary search, bubble sort, quicksort, heap sort |
| Combinatorics | Factorial, binomial coefficient, permutations, combinations |
| Sequences | Prime, Fibonacci, Bernoulli, stair-walk counts |
| Optimization | Stock profit, maximum subarray |
| Parsing | Roman numerals, shortest palindrome |
| Modeling | Bowling score |
| Arithmetic | Arbitrary-precision, 32-bit, and 64-bit rational numbers |

Implementations are in `src\SixDemonBag`; matching xUnit tests are in
`tests\SixDemonBag.Tests`. Invalid inputs are rejected with standard .NET
exceptions.

## Build and test

Requires the .NET 10 SDK.

```powershell
dotnet build .\six-demon-bag.sln --configuration Release
dotnet test .\six-demon-bag.sln --configuration Release
```

## Examples

```csharp
using SixDemonBag;

var index = BinarySearch.Iterative(new[] { 1, 3, 5, 7 }, 5);
var profit = MaxProfit.Calculate([10, 22, 5, 75, 65, 80], 2);
var roman = RomanNumerals.Format(998);
var primes = Primes.Sequence.Take(10);
```
