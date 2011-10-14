using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class AttackEffect : Effect
	{
		private readonly TargetableEntityBehaviour targetable;
		public AttackEffect(TargetableEntityBehaviour targetable) {
			this.targetable = targetable;
		}

		#region implemented abstract members of HappyPenguin.Effects.Effect

		public override void Start(GameWorldBehaviour world) {
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

