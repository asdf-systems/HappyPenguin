using System;
using UnityEngine;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Entities
{
	public static class EntityStateGenerator
	{	
		public static EntityState CreateDefaultMovementState(EntityBehaviour target, float baseline)
		{
			var state = new EntityState("creature_movement");
			state.AnimationNames.Add("swim");
			state.Controllers.Add(new LinearObjectFollowMovementController(target));
			state.Controllers.Add(new FloatController(baseline));
			return state;
		}
		
		public static EntityState CreatePatrolState(Vector3 patrolPosition)
		{
			var state = new EntityState("patrol");
			state.Controllers.Add(new LinearMovementController(patrolPosition));
			return state;
		}
		
		public static EntityState CreatePerkMovementState(Vector3 target)
		{
			var state = new EntityState("perk_movement");
			state.Controllers.Add(new LinearMovementController(target));
			state.Controllers.Add(new FloatController(target.y));
			return state;
		}
		
		public static EntityState CreateDiveMovementState(EntityBehaviour entity, GameObject RetreatPoint, float timeInSeconds)
		{			
			var state = new EntityState("dive");
			state.AnimationNames.Add("swim");
			state.Controllers.Add(new ArcMovementController(entity.transform.position, RetreatPoint.transform.position , timeInSeconds));
			return state;
		}
	}
}

