using System;
using UnityEngine;
namespace Pux.Effects
{
	public sealed class CreatureSlowdownEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world){

			world.PlayFastBackgroundMusic();
			world.ModifyCreatures((x) => x.Speed *= 0.2f);
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.PlayNormalBackgroundMusic();
			world.ModifyCreatures((x) => x.Speed = x.DefaultSpeed);
		}
		
		#endregion
		public CreatureSlowdownEffect() {
			Duration = TimeSpan.FromSeconds(8);
			IconResourceUV = new Rect(0,197,144,144);
		}
		
		public override string Description {
			get {
				return "Bullet Time!";
			}
		}
	}
}

