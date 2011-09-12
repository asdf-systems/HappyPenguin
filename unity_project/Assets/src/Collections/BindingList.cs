using UnityEngine;
using System;
using System.Collections.Generic;

namespace HappyPenguin.Collections
{
	public sealed class BindingList<T> : List<T>
	{

		public new void Add (T item) {
			base.Add (item);
		}

		public new bool Remove (T item) {
			return base.Remove (item);
			
		}

		public event EventHandler ListChanged;
		private void InvokeListChanged () {
			var handler = ListChanged;
			if (handler == null) {
				return;
			}
			
			var e = new ListChangedEventArgs ();
			ListChanged (this, e);
		}
	}
	
}
