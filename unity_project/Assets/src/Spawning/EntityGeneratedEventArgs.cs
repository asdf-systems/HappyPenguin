using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public sealed class EntityGeneratedEventArgs<T> : EventArgs where T : TargetableEntityBehaviour
	{
		public EntityGeneratedEventArgs (T entity) {
			Entity = entity;
		}
		
		public T Entity {
			get;
			private set;
		}
	}
}

