using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public sealed class ArcObjectFollowMovementController : Controller<EntityBehaviour>
	{
		#region Fields and Properties
		public Vector3 ThrowStartPosition { get; private set; }

		public Vector3 ThrowEndPosition { get; private set; }



		#endregion
		public ArcObjectFollowMovementController (Vector3 initialDirection, EntityBehaviour target)
		{
		}

		protected override void UpdateOverride (EntityBehaviour entity)
		{
			throw new NotImplementedException ();
		}
	}
}

