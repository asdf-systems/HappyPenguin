using System;
using Pux.Entities;

namespace Pux.Effects
{
	public sealed class HideSymbolsEffect : Effect
	{
		private readonly TargetableEntityBehaviour entity;

		public HideSymbolsEffect(TargetableEntityBehaviour entity) {
			this.entity = entity;
		}

		#region implemented abstract members of Pux.Effects.Effect

		public override void Start(GameWorldBehaviour world) {
			entity.HideSymbols();
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

