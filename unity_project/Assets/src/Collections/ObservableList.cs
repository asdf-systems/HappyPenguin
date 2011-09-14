using UnityEngine;
using System;
using System.Collections.Generic;

namespace HappyPenguin.Collections
{
	public sealed class ObservableList<T> : List<T>
	{
		public new void Add (T item) {
			base.Add(item);
		}

		public new bool Remove (T item) {
			return base.Remove (item);
		}
		
		public event EventHandler<ItemEventArgs<T>> ItemRemoved;
		private void InvokeItemRemoved(T item)
		{
			var handler = ItemRemoved;
			if (handler == null) {
				return;
			}
			
			var e = new ItemEventArgs<T>(item);
			ItemRemoved(this, e);
		}
		
		public event EventHandler<ItemEventArgs<T>> ItemAdded;
		private void InvokeItemAdded(T item)
		{
			var handler = ItemAdded;
			if (handler == null) {
				return;
			}
			
			var e = new ItemEventArgs<T>(item);
			ItemAdded(this, e);
		}
	}
}
