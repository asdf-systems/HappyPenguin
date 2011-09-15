using System;
using UnityEngine;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin
{
	public sealed class StateGenerator
	{
		public StateGenerator() {
			
		}
		
		public EntityState CreateMovementState(EntityBehaviour entity, Vector3 to)
		{
			var state = new EntityState("movement");
			state.AnimationNames.Add("swim");
			var duration = TimeSpan.FromSeconds(10);
			state.Controllers.Add(new LinearMovementController(to, duration));
			return state;
		}
	}
}

