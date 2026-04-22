using System.Collections;

namespace Six.Demon.Bag.Lists;

public class LargeList<T> : ILargeList<T> {

	private const int ChunkBits = 20;
	private const int ChunkSize = 1 << ChunkBits;
	private const int ChunkMask = ChunkSize - 1;
	private readonly List<T[]> chunks = [];
	private long count;

	public long Count => count;

	int ICollection<T>.Count { get; }
	bool ICollection<T>.IsReadOnly { get; }

	T IList<T>.this[int index] {
		get => this[index];
		set => this[index] = value;
	}

	public T this[long index] {
		get {
			ValidateIndex(index);
			return GetChunk(index)[GetOffset(index)];
		}
		set {
			ValidateIndex(index);
			GetChunk(index)[GetOffset(index)] = value;
		}
	}

	public void Add(T item) {
		EnsureCapacityFor(count);
		this[count++] = item;
	}

	public void Clear() {
		chunks.Clear();
		count = 0;
	}

	public bool Contains(T item) =>
		IndexOf(item) >= 0;

	public long IndexOf(T item) {
		var comparer = EqualityComparer<T>.Default;

		for(var i = 0L; i < count; i++) {
			if(comparer.Equals(this[i], item))
				return i;
		}

		return -1;
	}

	public void Insert(long index, T item) {
		if(index < 0 || index > count)
			throw new ArgumentOutOfRangeException(nameof(index));

		// Slow, but correct. Insert in a giant logical array
		// necessarily means shifting.
		Add(default!);

		for(var i = count - 1; i > index; i--)
			this[i] = this[i - 1];

		this[index] = item;
	}

	public bool Remove(T item) {
		var index = IndexOf(item);
		if(index < 0)
			return false;

		RemoveAt(index);
		return true;
	}

	public void RemoveAt(long index) {
		ValidateIndex(index);

		for(var i = index; i < count - 1; i++)
			this[i] = this[i + 1];

		count--;

		// Clear released slot so references can be GC'd
		this[count] = default!;
	}

	public IEnumerator<T> GetEnumerator() {
		for(var i = 0L; i < count; i++)
			yield return this[i];
	}

	private void EnsureCapacityFor(long index) {
		var requiredChunk = index >> ChunkBits;

		while(chunks.Count <= requiredChunk)
			chunks.Add(new T[ChunkSize]);
	}

	private T[] GetChunk(long index) =>
		chunks[checked((int)(index >> ChunkBits))];

	private static int GetOffset(long index) =>
		(int)(index & ChunkMask);

	private void ValidateIndex(long index) {
		if(index < 0 || index >= count)
			throw new ArgumentOutOfRangeException(nameof(index));
	}

	int IList<T>.IndexOf(T item) {
		var index = this.IndexOf(item);
		return index > int.MaxValue ?
			throw new InvalidOperationException("Index exceeds int.MaxValue.") :
			(int)index;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	void IList<T>.Insert(int index, T item) => this.Insert(index, item);
	void IList<T>.RemoveAt(int index) => this.RemoveAt(index);
	void ICollection<T>.CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();
}