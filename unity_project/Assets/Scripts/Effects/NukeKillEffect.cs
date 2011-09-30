using System;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class NukeKillEffect : Effect {
		
		private TargetableEntityBehaviour entity;
		
		public NukeKillEffect (TargetableEntityBehaviour e) : base(){
			entity = e;
		}
		
		public override void Start(GameWorldBehaviour w){
			
			entity.HideSymbols();
			entity.animation.Play("explode");
			var creatures = w.entityManager.FindTargetables();
			foreach (var creature in creatures) {
				List<Effect> killEffects = creature.CollectedEffects;
				foreach (Effect effect in killEffects) {
				w.effectManager.RegisterEffect(effect);			
				}
			}
		}	
		public override void Update(GameWorldBehaviour w){
			//instant effect
		}
		
		public override void Stop(GameWorldBehaviour w){
			//w.entityManager.VoidTargetable(entity);
		}
	}
}