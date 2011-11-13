using System;
using Pux.Entities;

namespace Pux.Effects
{
	public sealed class PointsEffect : Effect
	{
		public float PointChange { get; private set; }
		public CreatureBehaviour Creature { get; set; }
		public int amount;

		public PointsEffect(float pointValue, CreatureBehaviour creature) : base() {
			PointChange = pointValue;
			Creature = creature;
		}

		public override void Start(GameWorldBehaviour world) {
			amount = (int) (PointChange * world.PointsMultiplier);
			world.ChangePlayerPoints(amount);
		}

		public override void Update(GameWorldBehaviour world) {
			// is instant effect
		}

		public override void Stop(GameWorldBehaviour world) {
			// is instant effect
		}
		
		public override string Description {
			get {
				return string.Format("{0} bling blings", amount);
			}
		}
	}
}
