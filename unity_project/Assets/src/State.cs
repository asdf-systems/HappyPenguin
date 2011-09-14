using System;
using System.Collections.Generic;


namespace HappyPenguin
{
	public class State
	{
		public State () {
			
		}
		
		public IEnumerable<string> AnimationNames {
			get;
			private set;
		}
		
		public IEnumerable<Controller> Controllers {
			get;
			private set;
		}
	}
}

