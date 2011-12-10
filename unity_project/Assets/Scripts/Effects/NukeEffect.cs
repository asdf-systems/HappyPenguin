using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Pux.Entities;
using Pux.Controllers;

namespace Pux.Effects
{
	public sealed class NukeEffect : Effect
	{
		private TargetableEntityBehaviour entity;

		public NukeEffect(TargetableEntityBehaviour e) : base() {
			entity = e;
		}
		
		public override string Description {
			get {
				return "Boo!!";
			}
		}

		public override void Start(GameWorldBehaviour w) {
			if(entity == null)
				return;
			if(entity.audio == null)
				return;
			entity.audio.clip = entity.DeathSound;
			entity.audio.Play();
			entity.ClearControllers();
			entity.QueueController("float", new FloatController(Environment.SeaLevel));
			entity.transform.LookAt(Camera.main.transform);
			
			var delay = TimeSpan.FromMilliseconds(1500);
			w.ApplyEffect(new DelayedEffect(new SinkEffect(entity), delay));
			
			entity.HideSymbols();
			entity.PlayAnimation("explode");
			
			var creatures = w.entityManager.GetAttackingCreatures();
			foreach (var creature in creatures.Where(x => !(x == entity))) {
				var killEffects = creature.HitEffects;
				foreach (var effect in killEffects) {
					w.ApplyEffect(effect);
				}
			}
		}
		public override void Update(GameWorldBehaviour w) {
			//instant effect
		}

		public override void Stop(GameWorldBehaviour w) {
			//w.entityManager.VoidTargetable(entity);
		}
	}
}
