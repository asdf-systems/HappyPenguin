using System;
using Pux.Entities;

namespace Pux.Spawning
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

