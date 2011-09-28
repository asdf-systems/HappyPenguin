using System;
using HappyPenguin.Controllers;

namespace HappyPenguin.Controllers
{
	public class AllControllersFinishedEventArgs<T> : EventArgs
	{
		public AllControllersFinishedEventArgs (T entity)
		{
			EntityType = entity;

		}
		public T EntityType {
			get;
			private set;
		}
	}
}

