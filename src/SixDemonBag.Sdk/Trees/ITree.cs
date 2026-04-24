using System;
using System.Collections.Generic;
using System.Text;

namespace Six.Demon.Bag.Trees;

public interface ITree<T> {
	ITree<T>? Parent { get; }
	T Value { get; set; }
	IEnumerable<ITree<T>> Children { get; }
	ITree<T> this[int index] { get; set; }
}