using System;
using System.Collections.Generic;
using HappyPenguin.Controllers;
using HappyPenguin.Collections;

namespace HappyPenguin.Entities
{
	public sealed class EntityControlManager
	{
		private readonly List<string> keysToBeRemoved;
		private readonly Dictionary<string, Controller> controllers;
		private readonly Dictionary<string, Controller> queuedControllers;
		
		public EntityControlManager() {
			queuedControllers = new Dictionary<string, Controller>();
			controllers = new Dictionary<string, Controller>();
			keysToBeRemoved = new List<string>();
		}
		
		public void ClearControllers()
		{
			controllers.Clear();	
		}
		
		public void QueueController(string name, Controller controller)
		{
			queuedControllers.Add(name, controller);
		}
		
		private void AddController(string name, Controller controller)
		{
			if (controllers.ContainsKey(name)) {
				var message = string.Format("must remove controller with name {0}, before attaching a second with the same name.");
				throw new ApplicationException(message);
			}
			
			controller.ControllerFinished += (sender, e) => keysToBeRemoved.Add(name);
			controllers.Add(name, controller);
		}
		
		public bool RemoveController(string name)
		{
			return controllers.Remove(name);
		}
		
		public bool ContainsController(string name)
		{
			return controllers.ContainsKey(name);
		}
		
		public void Update(EntityBehaviour entity)
		{
			foreach (var c in Controllers) {
				c.Update(entity);
			}
			
			foreach (var oc in keysToBeRemoved) {	
				RemoveController(oc);
			}
			keysToBeRemoved.Clear();
			
			foreach (var qc in queuedControllers) {
				this.AddController(qc.Key, qc.Value);
			}
			queuedControllers.Clear();
		}
		
		public IEnumerable<Controller> Controllers {
			get{ return controllers.Values; }
		}
	}
}

