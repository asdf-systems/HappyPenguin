using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class DeathEffect : Effect {
		
		private TargetableEntityBehaviour entity;
		
		public DeathEffect (TargetableEntityBehaviour e) : base(){
			entity = e;
			Duration = TimeSpan.Zero.Add(TimeSpan.FromSeconds(10));
		}
		
		public override void Start(GameWorldBehaviour w){
			var retreatPoint = w.RetreatPoint;
			entity.CurrentState = EntityStateGenerator.CreateDiveMovementState(entity, retreatPoint, (float)Duration.TotalSeconds);
		}
		
		public override void Update(GameWorldBehaviour w){
			
		}
		
		public override void Stop(GameWorldBehaviour world){
			world.entityManager.VoidTargetable(entity);
		}
	}
}

