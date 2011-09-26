using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Entities
{
	public class EntityState
	{	
		
		public EntityState (string name) {
			Name = name;
			AnimationNames = new List<string>();
			Controllers = new List<Controller<EntityBehaviour>>();
		}
		
		public string Name {
			get;
			private set;
		}
		
		public IList<string> AnimationNames {
			get;
			private set;
		}
		
		public IList<Controller<EntityBehaviour>> Controllers {
			get;
			private set;
		}
		
		public virtual void Update(EntityBehaviour entity){
			foreach (var controller in Controllers) {
				controller.Update(entity);
			}
		}
		
		public virtual void RemoveControllersByType<T>() where T : Controller<EntityBehaviour>
		{
			var controllers = Controllers.Where(x => x is T).ToList();
			foreach (var  c in controllers) {
				Controllers.Remove(c);
			}
		}
		
		public virtual void Start(EntityBehaviour entity)
		{
			foreach (var animationName in AnimationNames) {
				Debug.Log(animationName);
				var animation = entity.gameObject.animation[animationName];
				if (animation == null) {
					Debug.Log("Warning: Missing Animationname: " + animation);
				}
				else {
					animation.wrapMode = UnityEngine.WrapMode.Loop;
					entity.animation.CrossFade(animationName);
				}
				
			}
		}
		
		public virtual void Stop(EntityBehaviour entity)
		{
			foreach (var animationName in AnimationNames) {
				entity.animation.Stop(animationName);
			}
		}
		
		public event EventHandler<AllControllersFinishedEventArgs<T>> AllControllersFinished;
		protected void InvokeAllControllersFinished (T entity) {
			var handler = AllControllersFinished;
			if (handler == null) {
				return;
			}
			
			var e = new AllControllersFinishedEventArgs<T> (entity);
			AllControllersFinished (this, e);
		}
		
		public virtual void OnControllerFinished(){
			
		}
		
	}
}