using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Effects
{
	public sealed class PointEffect : Effect
	{

		public float PointChange { get; private set; }
		public CreatureBehaviour Creature { get; set; }

		public PointEffect(float pointValue, CreatureBehaviour creature) : base() {
			PointChange = pointValue;
			Creature = creature;
		}

		public override void Start(GameWorldBehaviour world) {
			world.ChangePlayerPoints(PointChange);
			Creature.audio.clip = Creature.DeathSound;
			Creature.audio.Play();
		}

		public override void Update(GameWorldBehaviour world) {
			// is instant effect
		}

		public override void Stop(GameWorldBehaviour world) {
			// is instant effect
		}
	}
}
