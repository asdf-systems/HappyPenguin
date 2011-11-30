using System;
using UnityEngine;

namespace Pux.Effects
{
	public abstract class Effect
	{
		public Effect() {
			Duration = EffectDuration.Instant;
			IsPositive = true;
			IconResourceUV = new Rect(0,0,0,0);
		}
		
		public bool IsPositive {
			get;
			set;
		}
		public virtual bool IsStackable { get { return true; }}

		public abstract void Start(GameWorldBehaviour world);
		public abstract void Update(GameWorldBehaviour world);
		public abstract void Stop(GameWorldBehaviour world);
		public virtual string Description { get{ return string.Empty;} }
		public bool HasDescription {
			get { return !string.IsNullOrEmpty(Description); }
		}
		//public bool IsIconAvailable { get { return !string.IsNullOrEmpty(IconResourceUV); }}
		public bool IsIconAvailable { 
			get { 
				return (IconResourceUV.x != 0 || IconResourceUV.y != 0 || IconResourceUV.width != 0 || IconResourceUV.height != 0);
			}
		}
		
		public TimeSpan Duration { get; internal protected set; }

		public Rect IconResourceUV { get; protected set; }

		public bool IsExpired(TimeSpan startTime) {
			var current = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
			return current - startTime >= this.Duration;
		}
	}
}

