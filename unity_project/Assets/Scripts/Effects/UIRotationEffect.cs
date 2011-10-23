using System;

namespace HappyPenguin.Effects
{
	public sealed class UIRotationEffect : Effect
	{
		private readonly ClockRotations[] clockRotations;
		private readonly Random random;
		
		public UIRotationEffect ()
		{
			random = new Random();
			clockRotations = new ClockRotations[2];
		}
		
		#region implemented abstract members of HappyPenguin.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			var value = random.Next(0, 100);
			if (value > 50) {
				clockRotations[0] = ClockRotations.Clockwise;
				clockRotations[1] = ClockRotations.CounterClockwise;
			} else {
				clockRotations[0] = ClockRotations.CounterClockwise;
				clockRotations[1] = ClockRotations.Clockwise;
			}
			
			world.InvokeUIRotation(clockRotations[0]);
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			throw new System.NotImplementedException();
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.InvokeUIRotation(clockRotations[1]);
		}
		
		#endregion
	}
}

