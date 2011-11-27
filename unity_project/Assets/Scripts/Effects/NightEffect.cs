using System;
using UnityEngine;

namespace Pux.Effects
{
	public sealed class NightEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			RenderSettings.ambientLight = new Color(0.2f,0.2f,0.2f,0.7f);
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			RenderSettings.ambientLight = new Color(1,1,1,1);
		}
		
		public override string Description {
			get {
				return "Darkness ...";
			}
		}
		
		#endregion
		public NightEffect() {
			Duration = TimeSpan.FromSeconds(6);
			IconResourceKey = "UI/EffectIcons/night";
		}
	}
}

