using System;
using System.Collections.Generic;
using HappyPenguin.Controllers;
using HappyPenguin.Collections;

namespace HappyPenguin.Entities
{
	public sealed class EntityControlManager
	{
		private readonly List<string> keysToBeRemoved;
		private readonly Dictionary<string, EntityController> controllers;
		private readonly Dictionary<string, EntityController> queuedControllers;

		public EntityControlManager() {
			queuedControllers = new Dictionary<string, EntityController>();
			controllers = new Dictionary<string, EntityController>();
			keysToBeRemoved = new List<string>();
		}

		public void ClearControllers() {
			controllers.Clear();
		}

		public void QueueController(string name, EntityController controller) {
			if (queuedControllers.ContainsKey(name)) {
				queuedControllers.Remove(name);
			}
			queuedControllers.Add(name, controller);
		}

		private void AddController(string name, EntityController controller) {
			if (controllers.ContainsKey(name)) {
				var message = string.Format("must remove controller with name {0}, before attaching a second with the same name.");
				throw new ApplicationException(message);
			}
			
			controller.ControllerFinished += (sender, e) => keysToBeRemoved.Add(name);
			controllers.Add(name, controller);
		}

		public void DequeueController(string name) {
			keysToBeRemoved.Add(name);
		}

		public bool IsControllerAttached(string name) {
			return controllers.ContainsKey(name);
		}

		public void Update(EntityBehaviour entity) {
			foreach (var c in Controllers) {
				c.Update(entity);
			}
			
			foreach (var oc in keysToBeRemoved) {
				controllers.Remove(oc);
			}
			keysToBeRemoved.Clear();
			
			foreach (var qc in queuedControllers) {
				this.AddController(qc.Key, qc.Value);
			}
			queuedControllers.Clear();
		}

		public IEnumerable<EntityController> Controllers {
			get { return controllers.Values; }
		}
	}
}

