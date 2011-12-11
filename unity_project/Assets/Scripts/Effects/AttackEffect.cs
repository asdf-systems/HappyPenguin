using System;
using Pux.Entities;

namespace Pux.Effects
{
	public sealed class AttackEffect : Effect
	{
		private readonly TargetableEntityBehaviour targetable;
		public AttackEffect(TargetableEntityBehaviour targetable) {
			this.targetable = targetable;
		}

		#region implemented abstract members of Pux.Effects.Effect

		public override void Start(GameWorldBehaviour world) {
			if(targetable == null)
				return;
			if(targetable.audio == null)
				return;
			
			targetable.audio.clip = targetable.AttackSound;
			targetable.audio.Play();
		}

		public override void Update(GameWorldBehaviour world) {
			// nada
		}


		public override void Stop(GameWorldBehaviour world) {
			// nada
		}
		
		#endregion
	}
}

