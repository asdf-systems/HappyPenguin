using System;
using UnityEngine;

namespace Pux.Effects
{
	public sealed class FastMotionEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			Time.timeScale = 1.2f;
			GUIManager.Instance.Alert("fast motion");
			world.PlayFastBackgroundMusic();
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			Time.timeScale = 1;
			world.PlayNormalBackgroundMusic();
		}
		
		#endregion
		public FastMotionEffect() {
			Duration = TimeSpan.FromSeconds(9);
			IconResourceUV = new Rect(1280,197,144,144);
			IsPositive = false;
		}
		
		public override string Description {
			get {
				return string.Empty;
			}
		}
	}
}

