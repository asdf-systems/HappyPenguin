using System;
using HappyPenguin.Controllers;

namespace HappyPenguin.Controllers
{
	public class ControllerFinishedEventArgs<T> : EventArgs
	{
		public ControllerFinishedEventArgs (T entity)
		{
			EntityType = entity;
		}
		public T EntityType {
			get;
			private set;
		}
	}
}

