using System;
using UnityEngine;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Entities
{
	public static class EntityStateGenerator
	{	
		public static EntityState CreateDefaultMovementState(EntityBehaviour target)
		{
			var state = new EntityState("movement");
			state.AnimationNames.Add("swim");
			state.Controllers.Add(new LinearObjectFollowMovementController(target));
			state.Controllers.Add(new FloatController(target.Position.y));
			return state;
		}
		
		public static EntityState CreatePatrolState(Vector3 patrolPosition)
		{
			var state = new EntityState("patrol");
			state.Controllers.Add(new LinearMovementController(patrolPosition));
			return state;
		}
	}
}

