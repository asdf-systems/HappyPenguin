using System;

namespace HappyPenguin.Controllers
{
	public abstract class Controller<T>
	{
		protected Controller () {
			
		}
		
		public abstract void Update(T entity);
		
		public event EventHandler<ControllerFinishedEventArgs<T>> ControllerFinished;
		protected void InvokeControllerFinished (T entity) {
			var handler = ControllerFinished;
			if (handler == null) {
				return;
			}
			
			var e = new ControllerFinishedEventArgs<T> (entity);
			ControllerFinished (this, e);
		}
	}
	
	
}

