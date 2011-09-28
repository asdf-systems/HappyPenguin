using System;
using HappyPenguin.Controllers;

namespace HappyPenguin.Controllers
{
	public class StateFinishedEventArgs<T> : EventArgs
	{
		public StateFinishedEventArgs (T entity)
		{
			EntityType = entity;

		}
		public T EntityType {
			get;
			private set;
		}
	}
}

