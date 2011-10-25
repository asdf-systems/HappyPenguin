using System;
namespace Pux.Spawning
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

