using System.Collections;

namespace Six.Demon.Bag.Lists;

public class LargeList<T> : ILargeList<T> {
	private const int ChunkBits = 20;
	private const int ChunkSize = 1 << ChunkBits;
	private const int ChunkMask = ChunkSize - 1;

	private readonly List<T[]> chunks = [];
	private long count;

	public long Count => count;

	public T this[long index] {
		get {
			index = ValidateIndex(index);
			return GetChunk(index)[GetOffset(index)];
		}
		set {
			index = ValidateIndex(index);
			GetChunk(index)[GetOffset(index)] = value;
		}
	}

	public void Add(T item) {
		EnsureCapacityFor(count);
		GetChunk(count)[GetOffset(count)] = item;
		count++;
	}

	public void Clear() {
		chunks.Clear();
		count = 0;
	}

	public bool Contains(T item) => IndexOf(item) >= 0;

	public long IndexOf(T item) {
		var comparer = EqualityComparer<T>.Default;
		var remaining = count;
		var baseIndex = 0L;

		for(var chunkIndex = 0; chunkIndex < chunks.Count && remaining > 0; chunkIndex++) {
			var chunk = chunks[chunkIndex];
			var length = remaining > ChunkSize ? ChunkSize : (int)remaining;

			for(var i = 0; i < length; i++) {
				if(comparer.Equals(chunk[i], item))
					return baseIndex + i;
			}

			baseIndex += length;
			remaining -= length;
		}

		return -1;
	}

	public void Insert(long index, T item) {
		if(index < 0 || index > count)
			throw new ArgumentOutOfRangeException(nameof(index));

		if(index == count) {
			Add(item);
			return;
		}

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
		index = ValidateIndex(index);

		for(var i = index; i < count - 1; i++)
			this[i] = this[i + 1];

		count--;
		GetChunk(count)[GetOffset(count)] = default!;
	}



	public IEnumerator<T> GetEnumerator() {
		var remaining = count;

		for(var chunkIndex = 0; chunkIndex < chunks.Count && remaining > 0; chunkIndex++) {
			var chunk = chunks[chunkIndex];
			var length = remaining > ChunkSize ? ChunkSize : (int)remaining;

			for(var i = 0; i < length; i++)
				yield return chunk[i];

			remaining -= length;
		}
	}

	private void EnsureCapacityFor(long index) {
		ArgumentOutOfRangeException.ThrowIfNegative(index);

		var requiredChunk = index >> ChunkBits;

		while(chunks.Count <= requiredChunk)
			chunks.Add(new T[ChunkSize]);
	}

	private T[] GetChunk(long index) => chunks[checked((int)(index >> ChunkBits))];

	private static int GetOffset(long index) =>
		(int)(index & ChunkMask);

	private long ValidateIndex(long index) {
		if(index < 0 || index >= count)
			throw new ArgumentOutOfRangeException(nameof(index));

		return index;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}