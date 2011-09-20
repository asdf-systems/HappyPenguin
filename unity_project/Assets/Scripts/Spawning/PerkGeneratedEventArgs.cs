using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public sealed class PerkGeneratedEventArgs<T> : EventArgs
	{
		public PerkGeneratedEventArgs (T perk) {
			PerkType = perk;
		}
		
		public T PerkType {
			get;
			private set;
		}
	}
}

