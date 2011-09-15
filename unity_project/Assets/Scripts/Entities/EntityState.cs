using System;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin
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
	}
}

