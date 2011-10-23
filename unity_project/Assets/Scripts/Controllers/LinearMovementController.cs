using System;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public class LinearMovementController : EntityController
	{
		private TimeSpan elapsedTime;
		private readonly Vector3 targetPosition;
		private bool lookAt;
		
		public LinearMovementController(Vector3 targetPosition)
			: this(targetPosition, true) { }
		
		public LinearMovementController(Vector3 targetPosition, bool lookAt) {
			this.lookAt = lookAt;
		 	this.targetPosition = targetPosition;	
		}
	
		protected override void UpdateOverride (EntityBehaviour entity)
		{
			var isCloseEnough = entity.transform.position.IsCloseEnoughTo(targetPosition);
			if (isCloseEnough) {
				return;
			}
			
			elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
			
			// v = s / t
			// s = v * t
			var currentPosition = entity.transform.position;
			
			var direction = targetPosition - currentPosition;
			
			var normalizedDirection = direction;
			normalizedDirection.Normalize();
			
			var offset = entity.Speed * Time.deltaTime;
			var movementVector = normalizedDirection * offset;
			
			entity.transform.position = entity.transform.position + movementVector; 
			if (lookAt) {
				entity.gameObject.transform.LookAt(entity.transform.position + normalizedDirection);	
			}
		}
	}
}

