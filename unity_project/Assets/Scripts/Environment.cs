using System;

namespace HappyPenguin
{
	public static class Environment
	{
		static Environment()
		{
			SeaLevel = -0.8256311f;
		}
		
		public static float SeaLevel {
			get;
			set;
		}
	}
}

