namespace Six.Demon.Bag.Lists;

public interface ILargeList<T> : IEnumerable<T>, IList<T> {
	new long Count { get; }
	T this[long index] { get; set; }
	new long IndexOf(T item);
	void Insert(long index, T item);
	void RemoveAt(long index);
}