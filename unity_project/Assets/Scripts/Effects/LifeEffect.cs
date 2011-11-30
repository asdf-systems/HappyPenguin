using System;

namespace Pux.Effects
{
	public sealed class LifeEffect : Effect
	{
		public float LifeChange { get; private set; }
		public LifeEffect(float lifeValue) : base() {
			LifeChange = lifeValue;
		}

		public override void Start(GameWorldBehaviour world) {
			world.SetPlayerHealth(LifeChange);
		}

		public override void Update(GameWorldBehaviour world) {
			// is instant effect
		}

		public override void Stop(GameWorldBehaviour world) {
			// is instant effect
		}
		
		public override string Description {
			get {
				return "Life +1";
			}
		}
	}
}

