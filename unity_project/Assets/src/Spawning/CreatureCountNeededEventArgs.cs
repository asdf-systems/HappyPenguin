using System;
namespace HappyPenguin.Spawning
{
	public class CreatureCountNeededEventArgs : EventArgs
	{
		
		public int CreatureCount {
			get;
			set;
		}
		public CreatureCountNeededEventArgs ()
		{
		}
	}
}

