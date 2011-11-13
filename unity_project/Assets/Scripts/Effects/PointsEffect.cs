using System;
using Pux.Entities;

namespace Pux.Effects
{
	public sealed class PointsEffect : Effect
	{
		public float PointChange { get; private set; }
		public CreatureBehaviour Creature { get; set; }

		public PointsEffect(float pointValue, CreatureBehaviour creature) : base() {
			PointChange = pointValue;
			Creature = creature;
		}

		public override void Start(GameWorldBehaviour world) {
			world.ChangePlayerPoints(PointChange * world.PointsMultiplier);
		}

		public override void Update(GameWorldBehaviour world) {
			// is instant effect
		}

		public override void Stop(GameWorldBehaviour world) {
			// is instant effect
		}
	}
}
