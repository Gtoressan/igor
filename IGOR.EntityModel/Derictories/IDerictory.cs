using System;
using System.Collections.Generic;

namespace IGOR.EntityModel.Derictories
{
	public interface IDerictory<T>
	{
		Exception Error { get; }

		IEnumerable<T> Items { get; }

		void Add(T item);

		void Drop(T item);

		void Edit(T item);

		T Find(params object[] keys);
	}
}
