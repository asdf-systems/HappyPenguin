using UnityEngine;
using System;
using System.Collections.Generic;

namespace HappyPenguin.Collections
{
	public sealed class ObservableList<T> : List<T>
	{

		public new void Add (T item) {
			base.Add (item);
		}

		public new bool Remove (T item) {
			return base.Remove (item);
			
		}
		
		public event EventHandler ItemRemoved;

		public event EventHandler ItemAdded;
		
	}
	
}
