using System;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public sealed class LinearObjectFollowMovementController : Controller
	{
		private TimeSpan elapsedTime;
		private GameObject target;
		
		public bool IsPitchLocked {
			get;
			set;
		} 
		
		public bool IsFinishedOnCatchup {
			get;
			set;
		}
		
		public LinearObjectFollowMovementController(GameObject target) {
			this.target = target;
			IsYAxisIgnored = true;
		}
		
		public bool IsYAxisIgnored {
			get;
			set;
		}
		
		private Vector3 CalculateDirectonalVector(Vector3 target, Vector3 current)
		{
			if (IsYAxisIgnored) {
				return new Vector3(target.x, current.y, target.z) - current;
			}
			return target - current;
		}
		
		protected override void UpdateOverride(EntityBehaviour entity)
		{
			var isCloseEnough = entity.transform.position.IsCloseEnoughTo(target.transform.position, IsYAxisIgnored);
			if (isCloseEnough) {
				if (IsFinishedOnCatchup) {
					InvokeControllerFinished(entity);	
				}
				return;
			}
			
			elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
			
			var currentPosition = entity.transform.position;
			var targetPosition = target.transform.position;
			
			var direction = CalculateDirectonalVector(targetPosition, currentPosition);
			
			var normalizedDirection = direction;
			normalizedDirection.Normalize();
			
			var offset = entity.Speed * Time.deltaTime;
			var movementVector = normalizedDirection * offset;
			
			entity.transform.position = entity.transform.position + movementVector;
			
			var lookAtCoords = target.transform.position;
			if (IsPitchLocked) {
				lookAtCoords.y = entity.transform.position.y;
			}
			entity.transform.LookAt(lookAtCoords);
		}
	}
}

