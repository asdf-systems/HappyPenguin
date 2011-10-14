using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class DeathEffect : Effect {
		
		private TargetableEntityBehaviour entity;
		
		public DeathEffect (TargetableEntityBehaviour e) : base(){
			entity = e;
			Duration = new TimeSpan(0, 0, 10);
		}
		
		public override void Start(GameWorldBehaviour w){
			var creatureRetreat = GameObjectRegistry.GetObject("creature_retreat");
			if (entity is CreatureBehaviour) {
				var cb = entity as CreatureBehaviour;
				cb.NotCollectedEffects.RemoveAll(item => item is AttackAnimationEffect);
			}
			entity.HideSymbols();
			entity.Dive(creatureRetreat, 1000);
		}
		
		public override void Update(GameWorldBehaviour w){
			
		}
		
		public override void Stop(GameWorldBehaviour world){
			world.entityManager.VoidTargetable(entity);
		}
	}
}

