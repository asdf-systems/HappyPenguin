using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class PerkMoveEffect : Effect {
		
		private TargetableEntityBehaviour entity;
		private Vector3 target;
		
		public PerkMoveEffect (TargetableEntityBehaviour e, Vector3 t) : base(){

		}
		
		public override void Start(GameWorldBehaviour w){
		}
		
		public override void Update(GameWorldBehaviour w){
			
		}
		
		public override void Stop(GameWorldBehaviour world){
			
		}
	}
}
