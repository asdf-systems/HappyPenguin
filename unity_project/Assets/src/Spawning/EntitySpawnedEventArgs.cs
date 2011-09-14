using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public sealed class EntitySpawnedEventArgs<T> : EventArgs where T : TargetableEntityBehaviour
	{
		public EntitySpawnedEventArgs (T entity) {
			Entity = entity;
		}
		
		public T Entity {
			get;
			private set;
		}
	}
}

