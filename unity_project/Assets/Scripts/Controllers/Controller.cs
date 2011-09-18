using System;

namespace HappyPenguin.Controllers
{
	public abstract class Controller<T>
	{
		protected Controller () {
			
		}
		
		public abstract void Update(T entity);
	}
}

