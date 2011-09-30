using System;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class HealthKillEffect : Effect {
		private TargetableEntityBehaviour entity;
		private float lifeGain;
		
		public HealthKillEffect (TargetableEntityBehaviour e, float LifeGain) : base(){
			entity = e;
			lifeGain = LifeGain;
		}
		
		public override void Start(GameWorldBehaviour w){
			
			entity.HideSymbols();
			w.entityManager.VoidTargetable(entity);
			w.ChangePlayerHealth(lifeGain);
		}
		
		public override void Update(GameWorldBehaviour w){
			//instant effect
		}
		
		public override void Stop(GameWorldBehaviour w){
			//instant effect
		}
	}
}

