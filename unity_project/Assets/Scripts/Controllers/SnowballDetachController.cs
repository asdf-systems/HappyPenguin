using System;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Controllers;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public sealed class SnowballDetachController : Controller<EntityBehaviour>
	{
		private readonly Vector3 detachPoint;
		private readonly EntityBehaviour snowball;
		
		public SnowballDetachController(EntityBehaviour snowball, Vector3 detachPoint) {
			this.snowball = snowball;
			this.detachPoint = detachPoint;
			Trigger =  IsHammerTime;
		}
		
		private bool IsHammerTime()
		{
			return snowball.transform.position.IsCloseEnoughTo(detachPoint, false);
		}
		
		#region implemented abstract members of HappyPenguin.Controllers.Controller[EntityBehaviour]
		protected override void UpdateOverride (EntityBehaviour entity)
		{
			snowball.transform.parent = null;
			InvokeControllerFinished(entity);
		}
		
		#endregion
	}
}

