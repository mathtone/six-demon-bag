namespace Six.Demon.Bag.Trees;

using System.Collections;

public class Tree<T>(T value) : IList<Tree<T>>, ITree<T> {
	private Tree<T>? parent;
	private readonly List<Tree<T>> children = [];
	private T value = value;

	public Tree<T>? Parent => parent;
	public int Count => children.Count;
	public bool IsReadOnly => false;

	public T Value {
		get => value;
		set => this.value = value;
	}

	public Tree<T> this[int index] {
		get => children[index];
		set {
			ArgumentNullException.ThrowIfNull(value);

			var old = children[index];
			if(ReferenceEquals(old, value))
				return;

			ValidateCanAttach(value);
			DetachFromParent(value);

			old.parent = null;
			value.parent = this;
			children[index] = value;
		}
	}

	public void Add(Tree<T> item) {
		ArgumentNullException.ThrowIfNull(item);

		ValidateCanAttach(item);
		DetachFromParent(item);

		item.parent = this;
		children.Add(item);
	}

	public void Clear() {
		foreach(var child in children)
			child.parent = null;

		children.Clear();
	}

	public bool Contains(Tree<T> item) => children.Contains(item);

	public void CopyTo(Tree<T>[] array, int arrayIndex) =>
		children.CopyTo(array, arrayIndex);

	public int IndexOf(Tree<T> item) => children.IndexOf(item);

	public void Insert(int index, Tree<T> item) {
		ArgumentNullException.ThrowIfNull(item);

		ValidateCanAttach(item);
		DetachFromParent(item);

		item.parent = this;
		children.Insert(index, item);
	}

	public bool Remove(Tree<T> item) {
		if(!children.Remove(item))
			return false;

		item.parent = null;
		return true;
	}

	public void RemoveAt(int index) {
		var child = children[index];
		child.parent = null;
		children.RemoveAt(index);
	}

	public IEnumerator<Tree<T>> GetEnumerator() => children.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private void ValidateCanAttach(Tree<T> child) {
		if(ReferenceEquals(child, this))
			throw new InvalidOperationException("Cannot add a node to itself.");

		for(var node = this; node is not null; node = node.parent) {
			if(ReferenceEquals(node, child))
				throw new InvalidOperationException("Cannot create a cycle.");
		}
	}

	private static void DetachFromParent(Tree<T> child) {
		var oldParent = child.parent;

		if(oldParent is null)
			return;

		oldParent.children.Remove(child);
		child.parent = null;
	}

	public static implicit operator T(Tree<T> node) => node.Value;

	public static implicit operator Tree<T>(T value) => new(value);

	ITree<T>? ITree<T>.Parent => parent;
	IEnumerable<ITree<T>> ITree<T>.Children => children;
}
