using System;

namespace HappyPenguin.Effects
{
	public class PointEffect : Effect{
		public float pointChange{
			get;
			private set;
		}
		public PointEffect (float pointValue) : base(){
			pointChange = pointValue;
		}
		
		public override void Start(){
			TimeRemaining = TimeSpan.Zero;
			
		}
		
		public override void Update(GameWorldBehaviour world){
			world.ChangePlayerPoints(pointChange);
		}
		
		public override void Stop(){
			
		}
		
	}
}
