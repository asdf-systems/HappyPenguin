using System;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class PerkKillEffect : Effect {
		
		private TargetableEntityBehaviour entity;
		
		public PerkKillEffect (TargetableEntityBehaviour e) : base(){
			entity = e;

		}
		
		public override void Start(GameWorldBehaviour w){
			
			entity.HideSymbols();
			w.entityManager.VoidTargetable(entity);
			
		}
		
		public override void Update(GameWorldBehaviour w){
			//instant effect
		}
		
		public override void Stop(GameWorldBehaviour w){
			//instant effect
		}
	}
}

