using System;
namespace Pux.Effects
{
	public sealed class CreatureSlowdownEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			world.CreatureSpeedModifier = .8f;
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.CreatureSpeedModifier = 1.0f;
		}
		
		#endregion
		public CreatureSlowdownEffect() {
			Duration = TimeSpan.FromSeconds(4);
			IconResourceKey = "UI/EffectIcons/slow_creature";
		}
	}
}

