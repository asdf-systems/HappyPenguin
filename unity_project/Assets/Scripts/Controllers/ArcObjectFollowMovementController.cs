using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public sealed class ArcObjectFollowMovementController : Controller<EntityBehaviour>
	{
		public ArcObjectFollowMovementController(Vector3 initialDirection, EntityBehaviour target) {
		}
		
		public override void Update (EntityBehaviour entity)
		{
			throw new NotImplementedException ();
		}
	}
}

