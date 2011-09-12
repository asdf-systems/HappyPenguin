using System;

namespace HappyPenguin
{
	public abstract class Controller
	{
		public Controller () {
			
		}
		
		public abstract void Start();
		public abstract void Update(object @object);
		public abstract void Stop();
	}
}

