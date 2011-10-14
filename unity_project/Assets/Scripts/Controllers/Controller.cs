using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public abstract class Controller
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
		
		public virtual void Update(EntityBehaviour entity)
		{
			if (Trigger == null) {
				return;
			}
			
			IsTriggered = Trigger();
			if (IsTriggered) {
				UpdateOverride(entity);
			}
		}
		
		protected abstract void UpdateOverride(EntityBehaviour entity);
		
		public event EventHandler<BehaviourEventArgs<EntityBehaviour>> ControllerFinished;
		protected void InvokeControllerFinished (EntityBehaviour entity) {
			var handler = ControllerFinished;
			if (handler == null) {
				return;
			}
			
			var e = new BehaviourEventArgs<EntityBehaviour> (entity);
			ControllerFinished (this, e);
		}
	}
}

