using System;
using UnityEngine;
using Pux.Entities;

namespace Pux.Effects
{
	public sealed class SinkEffect : Effect
	{
		private readonly EntityBehaviour entity;
		
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			if(entity == null)
				return;
			if(entity.audio == null)
				return;
			
			entity.audio.clip = entity.DeathSound;
			entity.audio.Play();
			var ground = entity.transform.position + new Vector3(0, -100, 0);
			entity.Speed = 10;
			entity.MoveTo(ground, false);
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			// nada
		}
		
		#endregion
		public SinkEffect(EntityBehaviour entity) {
			this.entity = entity;		
		}
	}
}

