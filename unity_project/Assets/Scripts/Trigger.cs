using System;

namespace HappyPenguin
{
	public class Trigger
	{
		public Trigger() {
		}
		
		public Func<bool> Condition {
			get;
			set;
		}
		
		public Action Effect {
			get;
			set;
		}
	}
}

