using System;

namespace HappyPenguin.Controllers
{
	public abstract class Controller<T>
	{
		public Controller () {
			
		}
		
		public abstract void Start();
		public abstract void Update(T controlledObject);
		public abstract void Stop();
	}
}

