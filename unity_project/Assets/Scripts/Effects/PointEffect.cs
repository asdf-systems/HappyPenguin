using System;

namespace HappyPenguin.Effects
{
	public sealed class PointEffect : Effect {
		
		public float PointChange{
			get;
			private set;
		}
		
		public PointEffect (float pointValue) : base(){
			PointChange = pointValue;
		}
		
		public override void Start(GameWorldBehaviour world){
			world.ChangePlayerPoints(PointChange);
		}
		
		public override void Update(GameWorldBehaviour world){
			// is instant effect
		}
		
		public override void Stop(GameWorldBehaviour world){
			// is instant effect
		}
	}
}
