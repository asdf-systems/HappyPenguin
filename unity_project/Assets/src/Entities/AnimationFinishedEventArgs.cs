using System;
namespace HappyPenguin.Entities
{
	public sealed class AnimationFinishedEventArgs : EventArgs
	{
		public AnimationFinishedEventArgs (string animationName)
		{
			AnimationName = animationName;
		}
		
		public string AnimationName {
			get;
			 private set;
		}
		
	}
}

