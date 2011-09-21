using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class DeathAnimationFinishedEventArgs<T> : EventArgs
	{
		public DeathAnimationFinishedEventArgs(T entity) {
			EntityType = entity;
		}
		
		public T EntityType {
			get;
			private set;
		}
	}
}
