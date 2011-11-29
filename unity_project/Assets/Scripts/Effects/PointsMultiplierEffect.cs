using System;
using UnityEngine;
namespace Pux.Effects
{
	public sealed class PointsMultiplierEffect : Effect
	{
		private readonly float _multiplier;
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world){
			world.PointsMultiplier = _multiplier;
		}
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.PointsMultiplier = 1.0f;
		}
		
		#endregion
		public PointsMultiplierEffect(float multiplier) {
			_multiplier = multiplier;
			Duration = TimeSpan.FromSeconds(6);
			if (multiplier > 2) {
				IconResourceUV = new Rect(256,197,144,144);
			} else{
				IconResourceUV = new Rect(768,197,144,144);
				
			}
		}
		
		public override string Description {
			get {
				return _multiplier > 2 ? "Points x2" : "Points x3";
			}
		}
	}
}

