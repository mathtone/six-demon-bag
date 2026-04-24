namespace Six.Demon.Bag.Trees;

using System.Collections;
using System.Runtime.CompilerServices;

public class ThreadSafeTree<T>(T value) : IList<ThreadSafeTree<T>>, ITree<T> {
	private readonly Lock sync = new();

	private ThreadSafeTree<T>? parent;
	private readonly List<ThreadSafeTree<T>> children = [];
	private T value = value;

	public ThreadSafeTree<T>? Parent => sync.Locked(() => parent);

	public T Value {
		get => sync.Locked(() => value);
		set => sync.Locked(() => this.value = value);
	}

	public IEnumerable<ThreadSafeTree<T>> Children =>
		sync.Locked(() => children.ToArray());

	public ThreadSafeTree<T> this[int index] {
		get => sync.Locked(() => children[index]);
		set {
			ArgumentNullException.ThrowIfNull(value);

			WithLocks(this, value, () => {
				var old = children[index];

				if(ReferenceEquals(old, value))
					return;

				ValidateCanAttach(value);
				DetachFromParent(value);

				old.parent = null;
				value.parent = this;
				children[index] = value;
			});
		}
	}

	public int Count => sync.Locked(() => children.Count);

	public bool IsReadOnly => false;

	public void Add(ThreadSafeTree<T> item) {
		ArgumentNullException.ThrowIfNull(item);

		WithLocks(this, item, () => {
			ValidateCanAttach(item);
			DetachFromParent(item);

			item.parent = this;
			children.Add(item);
		});
	}

	public void Clear() =>
		sync.Locked(() => {
			foreach(var child in children)
				child.parent = null;

			children.Clear();
		});

	public bool Contains(ThreadSafeTree<T> item) =>
		sync.Locked(() => children.Contains(item));

	public void CopyTo(ThreadSafeTree<T>[] array, int arrayIndex) =>
		sync.Locked(() => children.CopyTo(array, arrayIndex));

	public int IndexOf(ThreadSafeTree<T> item) =>
		sync.Locked(() => children.IndexOf(item));

	public void Insert(int index, ThreadSafeTree<T> item) {
		ArgumentNullException.ThrowIfNull(item);

		WithLocks(this, item, () => {
			ValidateCanAttach(item);
			DetachFromParent(item);

			item.parent = this;
			children.Insert(index, item);
		});
	}

	public bool Remove(ThreadSafeTree<T> item) =>
		sync.Locked(() => {
			if(!children.Remove(item))
				return false;

			item.parent = null;
			return true;
		});

	public void RemoveAt(int index) =>
		sync.Locked(() => {
			var child = children[index];
			child.parent = null;
			children.RemoveAt(index);
		});

	public IEnumerator<ThreadSafeTree<T>> GetEnumerator() {
		var snapshot = sync.Locked(() => children.ToArray());
		return ((IEnumerable<ThreadSafeTree<T>>)snapshot).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private void ValidateCanAttach(ThreadSafeTree<T> child) {
		if(ReferenceEquals(child, this))
			throw new InvalidOperationException("Cannot add a node to itself.");

		for(var node = this; node is not null; node = node.parent)
			if(ReferenceEquals(node, child))
				throw new InvalidOperationException("Cannot create a cycle.");
	}

	private static void DetachFromParent(ThreadSafeTree<T> child) {
		var oldParent = child.parent;

		if(oldParent is null)
			return;

		oldParent.children.Remove(child);
		child.parent = null;
	}

	private static void WithLocks(ThreadSafeTree<T> a, ThreadSafeTree<T> b, Action action) {
		if(ReferenceEquals(a, b)) {
			a.sync.Locked(action);
			return;
		}

		var first = RuntimeHelpers.GetHashCode(a) < RuntimeHelpers.GetHashCode(b) ? a : b;
		var second = ReferenceEquals(first, a) ? b : a;

		lock(first.sync)
			lock(second.sync)
				action();
	}

	public static implicit operator T(ThreadSafeTree<T> node) => node.Value;

	public static implicit operator ThreadSafeTree<T>(T value) => new(value);

	ITree<T>? ITree<T>.Parent => Parent;

	IEnumerable<ITree<T>> ITree<T>.Children =>
		sync.Locked(() => children.Cast<ITree<T>>().ToArray());

	ITree<T> ITree<T>.this[int index] {
		get => this[index];
		set {
			if(value is not ThreadSafeTree<T> node)
				throw new ArgumentException($"Value must be of type {typeof(ThreadSafeTree<T>)}.", nameof(value));

			this[index] = node;
		}
	}
}