using System;
using UnityEngine;
namespace Pux.Effects
{
	public sealed class CreatureSlowdownEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world){

			world.PlayFastBackgroundMusic();
			Time.timeScale = 0.25f;
			GUIManager.Instance.Alert("BulletTime", 0.5f);
			//world.ModifyCreatures((x) => x.Speed *= 0.5f);
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.PlayNormalBackgroundMusic();
			Time.timeScale = 1;
			//world.ModifyCreatures((x) => x.Speed = x.DefaultSpeed);
		}
		
		#endregion
		public CreatureSlowdownEffect() {
<<<<<<< HEAD
			Duration = TimeSpan.FromSeconds(2);
=======
			Duration = TimeSpan.FromSeconds(5);
>>>>>>> origin/wieser/failed_push
			IconResourceUV = new Rect(0,197,144,144);
		}
		
		public override string Description {
			get {
				return string.Empty;
			}
		}
	}
}

