using System;
using Pux.Entities;

namespace Pux.Effects
{
	public sealed class RetreatEffect : Effect
	{
		private CreatureBehaviour creature;

		public RetreatEffect(CreatureBehaviour e) : base() {
			creature = e;
			Duration = new TimeSpan(0, 0, 10);
		}

		public override void Start(GameWorldBehaviour w) {
			creature.audio.clip = creature.DeathSound;
			creature.audio.Play();
			var creatureRetreat = GameObjectRegistry.GetObject("creature_retreat");
			creature.IsRetreating = true;
			creature.Speed = 30;
			creature.HideSymbols();
			creature.Dive(creatureRetreat, 1000);
		}

		public override void Update(GameWorldBehaviour w) {
			// nada	
		}

		public override void Stop(GameWorldBehaviour world) {
			
		}
	}
}

