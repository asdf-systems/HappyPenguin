using System;

namespace HappyPenguin.Effects
{
	public class LifeEffect : Effect{
		public float lifeChange{
			get;
			private set;
		}
		public LifeEffect (float lifeValue) : base(){
			lifeChange = lifeValue;
		}
		
		public override void Start(){
			TimeRemaining = TimeSpan.Zero;
			
		}
		
		public override void Update(GameWorldBehaviour world){
			world.ChangePlayerHealth(lifeChange);
		}
		
		public override void Stop(){
			
		}
		
	}
}

