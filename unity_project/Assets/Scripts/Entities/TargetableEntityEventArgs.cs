using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Entities
{
	public sealed class TargetableEntityEventArgs : EventArgs
	{
		public TargetableEntityEventArgs(TargetableEntityBehaviour entity) {
			Entity = entity;
		}
		
		public TargetableEntityBehaviour Entity {
			get;
			private set;
		}
	}
}

