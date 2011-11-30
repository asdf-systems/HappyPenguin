using System;
using UnityEngine;

namespace Pux.Effects
{
	public sealed class FastMotionEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			Time.timeScale = 1.5f;
			GUIManager.Instance.Alert("fast motion");
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			Time.timeScale = 1;
		}
		
		#endregion
		public FastMotionEffect() {
			IconResourceUV = new Rect(1280,197,144,144);
		}
		
		public override string Description {
			get {
				return string.Empty;
			}
		}
	}
}

