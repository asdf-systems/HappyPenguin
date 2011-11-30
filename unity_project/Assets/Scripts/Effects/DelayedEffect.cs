using UnityEngine;
using Pux.Effects;
using System;
using System.Collections;

namespace Pux.Effects{
	public sealed class DelayedEffect : Effect {
		
		private readonly Effect effect;
		
		public DelayedEffect (Effect effect, TimeSpan delay) {
			this.effect = effect;
			this.Duration = delay;
		}
		
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			// nada
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.ApplyEffect(effect);
		}
		
		#endregion
//		

	}
}

