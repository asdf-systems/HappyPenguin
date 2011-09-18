using System;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public sealed class LinearObjectFollowMovementController : MovementController
	{
		private TimeSpan elapsedTime;
		private EntityBehaviour target;
		
		public LinearObjectFollowMovementController(EntityBehaviour target) {
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
		
		public override void Update(EntityBehaviour entity)
		{
			var isCloseEnough = entity.Position.IsCloseEnoughTo(target.Position, IsYAxisIgnored);
			if (isCloseEnough) {
				return;
			}
			
			elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
			
			var currentPosition = entity.transform.position;
			var targetPosition = target.Position;
			
			var direction = CalculateDirectonalVector(targetPosition, currentPosition);
			
			var normalizedDirection = direction;
			normalizedDirection.Normalize();
			
			var offset = entity.Speed * Time.deltaTime;
			var movementVector = normalizedDirection * offset;
			
			entity.transform.position = entity.transform.position + movementVector; 
		}
		
	}
}

