using System;
using Pux.Entities;
using UnityEngine;

namespace Pux.Controllers
{
	public sealed class BalloonFloatingController : EntityController
	{
		private Vector3 startPosition;
		private float duration;
		
		private float speed;
		
		#region implemented abstract members of Pux.Controllers.Controller[Pux.Entities.EntityBehaviour]
		protected override void UpdateOverride (EntityBehaviour entity)
		{
			if (IsFinished || entity == null) {
				return;
			}
			
			duration += Time.deltaTime;
			
			var offset = Convert.ToSingle(Math.Sin(Time.timeSinceLevelLoad)) * 2.0f;
			var pos = entity.transform.position;
			entity.transform.position = new Vector3(startPosition.x + offset, pos.y, pos.z);
			
			var to = Quaternion.FromToRotation(entity.transform.up, Vector3.up);
			entity.transform.rotation =  Quaternion.Slerp(entity.transform.rotation, to, duration * speed);
		}
		
		#endregion
		public BalloonFloatingController(EntityBehaviour entity) {
			speed = 0.03f;
			startPosition = entity.transform.position;
		}
	}
}

