using System;
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
		
		public void Update(EntityBehaviour entity){
			foreach (var controller in Controllers) {
				controller.Update(entity);
			}
		}
		
		public void RemoveControllersByType<T>() where T : Controller<EntityBehaviour>
		{
			var controllers = Controllers.Where(x => x is T).ToList();
			foreach (var  c in controllers) {
				Controllers.Remove(c);
			}
		}
		
		public virtual void Start(EntityBehaviour entity)
		{
			foreach (var animationName in AnimationNames) {
				var animation = entity.gameObject.animation[animationName];
				animation.wrapMode = UnityEngine.WrapMode.Loop;
				entity.animation.CrossFade(animationName);
			}
		}
		
		public virtual void Stop(EntityBehaviour entity)
		{
			foreach (var animationName in AnimationNames) {
				entity.animation.Stop(animationName);
			}
		}
	}
}

