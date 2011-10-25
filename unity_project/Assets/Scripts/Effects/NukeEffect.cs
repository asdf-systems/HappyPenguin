using System;
using UnityEngine;
using System.Collections.Generic;
using Pux.Entities;

namespace Pux.Effects
{
	public sealed class NukeEffect : Effect
	{
		private TargetableEntityBehaviour entity;

		public NukeEffect(TargetableEntityBehaviour e) : base() {
			entity = e;
		}

		public override void Start(GameWorldBehaviour w) {
			entity.HideSymbols();
			entity.animation.Play("explode");
			var creatures = w.entityManager.FindCreatures();
			foreach (var creature in creatures) {
				var killEffects = creature.HitEffects;
				foreach (var effect in killEffects) {
					w.effectManager.RegisterEffect(effect);
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
