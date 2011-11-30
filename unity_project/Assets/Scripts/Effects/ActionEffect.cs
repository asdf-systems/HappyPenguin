using System;
	
namespace Pux.Effects
{
	public sealed class ActionEffect : Effect
	{
		private readonly Action action;
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			if (action != null) {
				action();
			}
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			
		}
		
		#endregion
		public ActionEffect(Action action) {
			this.action = action;
		}
	}
}

