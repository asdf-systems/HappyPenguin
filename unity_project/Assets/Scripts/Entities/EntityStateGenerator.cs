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
			state.Controllers.Add(new LinearObjectFollowMovementController(target){
				IsPitchLocked = true
			});
			state.Controllers.Add(new FloatController(baseline));
			return state;
		}
		
		public static EntityState CreatePatrolState(Vector3 patrolPosition)
		{
			var state = new EntityState("patrol");
			state.Controllers.Add(new LinearMovementController(patrolPosition));
			return state;
		}
		
		public static EntityState CreatePerkMovementState(TargetableEntityBehaviour entity, GameObject impactTarget, GameObject target2)
		{
			var batch = new StackedEntityState("stacked_state");
			
			var throwState = CreateDiveMovementState(entity, impactTarget, 1,50);
			throwState.StateFinished += batch.OnStateFinished;
			
			var floatState = new EntityState("perk_float");
			var impactController = new WaterImpactController(impactTarget.transform.position.y);
			floatState.Controllers.Add(impactController);
			floatState.Controllers.Add(new LinearMovementController(target2.transform.position));
			
			impactController.ControllerFinished += (sender, e)  => {
				floatState.Controllers.Remove(impactController);	
				floatState.Controllers.Add(new FloatController(impactTarget.transform.position.y));
			};
			
			batch.AddEntityState(throwState);
			batch.AddEntityState(floatState);
			return batch;
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

