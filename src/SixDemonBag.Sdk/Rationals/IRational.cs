using System.Numerics;

namespace Six.Demon.Bag.Rationals;

public interface IRational<T> : IComparable, IComparable<IRational<T>>, IEquatable<IRational<T>>
	where T : IBinaryInteger<T> {
	T Numerator { get; }
	T Denominator { get; }

	bool IsZero => Numerator == T.Zero;
	bool IsInteger => Denominator == T.One;
	int Sign => T.Sign(Numerator);

	void Deconstruct(out T numerator, out T denominator) {
		numerator = Numerator;
		denominator = Denominator;
	}
}
