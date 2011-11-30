using System;
using UnityEngine;

namespace Pux.Effects
{
	public sealed class UIRotationEffect : Effect
	{
		private readonly ClockRotations[] clockRotations;
		private readonly System.Random random;
		
		public UIRotationEffect ()
		{
			random = new System.Random();
			clockRotations = new ClockRotations[2];
			Duration = TimeSpan.FromSeconds(5);
			IconResourceUV = new Rect(1792,425,144,144);
		}
		
		public UIRotationEffect (ClockRotations clockRotation)
			: this()
		{
			clockRotations[0] = clockRotation;
		}
		
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world){
			world.IngameSounds.PlayBaddySound();
			var value = random.Next(0, 100);
			if (value > 50) {
				clockRotations[0] = ClockRotations.Clockwise;
				clockRotations[1] = ClockRotations.CounterClockwise;
			} else {
				clockRotations[0] = ClockRotations.CounterClockwise;
				clockRotations[1] = ClockRotations.Clockwise;
			}
			
			world.InvokeUIRotation(clockRotations[0], false);
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			// nothing to update
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.InvokeUIRotation(clockRotations[1], true);
		}
		
		#endregion
	}
}

