namespace Six.Demon.Bag.Lists;


public interface IReadOnlyLargeList<T> : IEnumerable<T> {
	long Count { get; }
	T this[long index] { get; }
	bool Contains(T item);
	long IndexOf(T item);
}

public interface ILargeList<T> : IReadOnlyLargeList<T> {
	new T this[long index] { get; set; }
	void Add(T item);
	void Clear();
	void Insert(long index, T item);
	bool Remove(T item);
	void RemoveAt(long index);
}