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