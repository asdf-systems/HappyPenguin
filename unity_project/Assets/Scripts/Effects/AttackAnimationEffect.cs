using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class AttackAnimationEffect : Effect {
		
		private TargetableEntityBehaviour entity;
		
		public AttackAnimationEffect (TargetableEntityBehaviour e) : base(){
			entity = e;
		}
		
		public override void Start(GameWorldBehaviour w){
			//TODO implement stuff
			//entity.animation.Play("attack");
			entity.audio.clip = entity.AttackSound;
			Debug.Log("ATTACKEEEE - implement stuff");
		}
		
		public override void Update(GameWorldBehaviour w){
			
		}
		
		public override void Stop(GameWorldBehaviour world){
			
		}
	}
}

