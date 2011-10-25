using UnityEngine;
using Pux.Controllers;
using System.Collections.Generic;
using System;
using Pux;
using Pux.Collections;

namespace Pux.Entities
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		private readonly ControlManager<EntityBehaviour> controlManager;

		public EntityBehaviour() {
			Speed = 10.0f;
			controlManager = new ControlManager<EntityBehaviour>();
		}

		public virtual new GameObject gameObject {
			get { return base.gameObject; }
		}

		public void Dispose() {
			InvokeGrimReaperAppeared();
		}

		public event EventHandler GrimReaperAppeared;
		private void InvokeGrimReaperAppeared() {
			var handler = GrimReaperAppeared;
			if (handler == null) {
				return;
			}
			handler(this, EventArgs.Empty);
		}

		public bool IsControllerAttached(string name) {
			return controlManager.IsControllerAttached(name);
		}

		public void ClearControllers() {
			controlManager.ClearControllers();
		}

		public void QueueController(string name, EntityController controller) {
			controlManager.QueueController(name, controller);
		}

		public void DequeueController(string name) {
			controlManager.DequeueController(name);
		}

		public IEnumerable<Controller<EntityBehaviour>> Controllers {
			get { return controlManager.Controllers; }
		}

		public void Awake() {
			AwakeOverride();
		}
		
		public void Start() {
			StartOverride();
		}

		public void Update() {
			UpdateOverride();
		}
		
		protected virtual void StartOverride(){
			// nada	
		}

		protected virtual void UpdateOverride() {
			controlManager.Update(this);
		}

		protected virtual void AwakeOverride() {
			// nada
		}
		
		public void Hide() {
			gameObject.active = false;
		}
	
		public void Show() {
			gameObject.active = true;
		}

		public float Speed;

		public AudioClip AttackSound;
		public AudioClip DeathSound;
		public AudioClip OtherSound;
	}
}
