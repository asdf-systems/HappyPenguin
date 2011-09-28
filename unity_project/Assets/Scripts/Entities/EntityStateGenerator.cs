using System;
using UnityEngine;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Entities
{
	public static class EntityStateGenerator
	{	
		public static EntityState CreateDefaultMovementState(GameObject target, float baseline)
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
		
		public static EntityState CreatePerkMovementState(TargetableEntityBehaviour entity, GameObject target1, GameObject target2)
		{
			var states = new StackedEntityState("perk_movement");
			var flightState = CreateDiveMovementState(entity, target1, 1,50);
			var diveState = new EntityState("dive"); //CreateDefaultMovementState(target2, entity.transform.position.y);
			flightState.StateFinished += states.OnStateFinished;
			diveState.Controllers.Add(new LinearObjectFollowMovementController(target2));
			//diveState.Controllers.Add(new FloatController(target1.transformy));
			states.AddEntityState(flightState);
			states.AddEntityState(diveState);
			return states;
		}
		
	public static EntityState CreateDiveMovementState(TargetableEntityBehaviour entity, GameObject RetreatPoint, float timeInSeconds, int flatness )
		{			
			var state = new EntityState("dive");
			state.AnimationNames.Add("swim");
			var arc = new ArcMovementController(entity, RetreatPoint , timeInSeconds , flatness);
			arc.ControllerFinished += state.OnControllerFinished;
			state.Controllers.Add(arc);
			return state;
		}


	}
}

