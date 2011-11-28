using System;
namespace Pux.Effects
{
	public sealed class SnowballSpeedModiferEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			world.SnowballSpeedModifier = 2.0f;
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.SnowballSpeedModifier = 1.0f;
		}
		
		#endregion
		public SnowballSpeedModiferEffect() {
			Duration = TimeSpan.FromSeconds(6);
			IconResourceKey = "UI/EffectIcons/snowball_speed";
		}
		
		public override string Description {
			get {
				return "Ball Speed x2";
			}
		}
	}
}

