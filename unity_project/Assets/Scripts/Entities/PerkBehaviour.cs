using System;
using HappyPenguin.Effects;
using System.Collections.Generic;
using HappyPenguin.Entities;


	public sealed class PerkBehaviour : TargetableEntityBehaviour
	{
		public PerkBehaviour () {
			Effects = new List<Effect>();
		}
		
		public IEnumerable<Effect> Effects {
			get;
			private set;
		}
	}


