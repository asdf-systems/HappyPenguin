using UnityEngine;
using Pux.Effects;
using System;
using System.Collections;

namespace Pux.Effects{
	public sealed class DelayedEffect : Effect {
		
		private readonly Effect effect;
		private TimeSpan time;
		
		public DelayedEffect (Effect effect, TimeSpan delay) {
			this.effect = effect;
			this.Duration = delay;
		}
		
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			this.time = TimeSpan.Zero;
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.effectManager.RegisterEffect(effect);
		}
		
		#endregion

	}
}

