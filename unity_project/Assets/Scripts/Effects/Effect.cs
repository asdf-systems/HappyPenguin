using System;
using UnityEngine;

namespace Pux.Effects
{
	public abstract class Effect
	{
		public Effect() {
			Duration = EffectDuration.Instant;
		}

		public abstract void Start(GameWorldBehaviour world);
		public abstract void Update(GameWorldBehaviour world);
		public abstract void Stop(GameWorldBehaviour world);

		public TimeSpan Duration { get; protected set; }
		
		public string IconResourceKey {
			get;
			protected set;
		}

		public bool IsExpired(TimeSpan startTime) {
			var current = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
			return current - startTime >= this.Duration;
		}
	}
}

