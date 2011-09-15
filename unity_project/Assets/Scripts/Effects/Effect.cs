using System;

namespace HappyPenguin.Effects
{
	public abstract class Effect
	{
		public Effect () {
			TimeRemaining = TimeSpan.Zero;	
		}
		
		public abstract void Start();
		public abstract void Update(GameWorldBehaviour world);
		public abstract void Stop();
		
		public TimeSpan TimeRemaining {
			get;
			private set;
		}
	}
}

