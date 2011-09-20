using System;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Collections;

namespace HappyPenguin.Effects
{
	public sealed class EffectManager
	{
		private readonly GameWorldBehaviour world;
		private Dictionary<Effect, TimeSpan> effects;
		
		public EffectManager (GameWorldBehaviour world) {
			this.world = world;
			effects = new Dictionary<Effect, TimeSpan>();
		}
		
		public void Update(){
			var expiredEffects = new List<Effect>();
			
			foreach(var e in effects) {
				var isExpired = e.Key.IsExpired(e.Value);
				if (isExpired) {
					expiredEffects.Add(e.Key);
					continue;
				}
				
				e.Key.Update(world);
			}
			
			foreach (var e in expiredEffects) {
				e.Stop(world);
				effects.Remove(e);
			}	
		}
		
		public void RegisterEffect(Effect e){
			effects.Add(e, TimeSpan.FromSeconds(Time.timeSinceLevelLoad));
			e.Start(world);
		}
		
		public IEnumerable<Effect> Effects {
			get{ return effects.Keys; }
		}
	}
}

