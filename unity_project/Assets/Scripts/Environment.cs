using System;

namespace Pux
{
	public static class Environment
	{
		static Environment()
		{
			SeaLevel = 4;//-0.5256311f;
		}
		
		public static float SeaLevel {
			get;
			set;
		}
	}
}

