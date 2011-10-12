using System;

namespace HappyPenguin.Controllers
{
	public abstract class Controller<T>
	{
		protected Controller () {
			// imediately active
			Trigger = () => true;
		}
		
		public Func<bool> Trigger {
			get;
			set;
		}
		
		public bool IsTriggered {
			get;
			set;
		}
		
		public virtual void Update(T entity)
		{
			if (Trigger == null) {
				return;
			}
			
			IsTriggered = Trigger();
			if (IsTriggered) {
				UpdateOverride(entity);
			}
		}
		
		protected abstract void UpdateOverride(T entity);
		
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

