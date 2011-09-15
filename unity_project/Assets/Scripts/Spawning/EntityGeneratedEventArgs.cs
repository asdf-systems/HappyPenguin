using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public sealed class EntityGeneratedEventArgs<T> : EventArgs
	{
		public EntityGeneratedEventArgs (T entity) {
			EntityType = entity;
		}
		
		public T EntityType {
			get;
			private set;
		}
	}
}

