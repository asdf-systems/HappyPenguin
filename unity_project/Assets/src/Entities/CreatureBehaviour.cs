using System;
using System.Collections.Generic;
using HappyPenguin.Effects;

namespace HappyPenguin.Entities
{
	public sealed class CreatureBehaviour : TargetableEntityBehaviour
	{
		public CreatureBehaviour () {
			AttackEffects = new List<Effect>();
			KillEffects = new List<Effect>();
		}
		
		public IEnumerable<Effect> AttackEffects {
			get;
			private set;
		}
		
		public IEnumerable<Effect> KillEffects {
			get;
			private set;
		}
	}
}

