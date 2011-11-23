using System;

namespace Pux
{
	public static class Environment
	{
		static Environment()
		{
			SeaLevel = 5;//-0.5256311f;
		}
		
		public static float SeaLevel {
			get;
			set;
		}
	}
}

