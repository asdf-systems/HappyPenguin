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
			entity.audio.clip = entity.DeathSound;
			entity.audio.Play();
			entity.HideSymbols();
			var retreatPoint = w.RetreatPoint;
			var flatness = 1000;
			entity.CurrentState = EntityStateGenerator.CreateDiveMovementState(entity, retreatPoint, (float)Duration.TotalSeconds, flatness);
		}
		
		public override void Update(GameWorldBehaviour w){
			
		}
		
		public override void Stop(GameWorldBehaviour world){
			world.entityManager.VoidTargetable(entity);
		}
	}
}

