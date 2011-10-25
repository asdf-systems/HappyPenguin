using System;

namespace Pux.Collections
{
	public sealed class ItemEventArgs<T> : EventArgs
	{
		internal ItemEventArgs (T item) 
		{
			Item = item;
		}
		
		public T Item {
			get; 
			private set;
		}
	}
}

