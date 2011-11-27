using System;
namespace Pux.Effects
{
	public sealed class CreatureSlowdownEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			world.ModifyCreatures((x) => x.Speed *= 0.8f);
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.ModifyCreatures((x) => x.Speed = x.DefaultSpeed);
		}
		
		#endregion
		public CreatureSlowdownEffect() {
			Duration = TimeSpan.FromSeconds(4);
			IconResourceKey = "UI/EffectIcons/slow_creature";
		}
		
		public override string Description {
			get {
				return "Bullet Time!";
			}
		}
	}
}

