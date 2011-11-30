using System;
using UnityEngine;
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
			Duration = TimeSpan.FromSeconds(8);
			IconResourceUV = new Rect(512,197,144,144);
		}
		
		public override string Description {
			get {
				return "Ball Speed x2";
			}
		}
	}
}

