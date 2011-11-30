using System;

namespace Pux
{
	public static class Environment
	{
		static Environment()
		{
			SeaLevel = 7.5f;//-0.5256311f;
		}
		
		public static float SeaLevel {
			get;
			set;
		}
	}
}

