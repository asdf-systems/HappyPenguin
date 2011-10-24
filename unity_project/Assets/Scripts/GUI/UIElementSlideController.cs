using System;
using UnityEngine;
using HappyPenguin.Unity2;

namespace HappyPenguin.UI
{
	public sealed class UIElementSlideController : UIElementController<GUIManager>
	{
		private readonly Vector2 targetPosition;
		private Func<float, float> function;

		public UIElementSlideController(Vector2 targetPosition)
			: this(targetPosition, (x) => x) { }
		
		public UIElementSlideController(Vector2 targetPosition, Func<float, float> function) {
			this.targetPosition = targetPosition;
			this.function = function;
		}
		
		protected override void UpdateOverride (UIElementBehaviour<GUIManager> entity)
		{	
			if (IsFinished) {
				return;
			}
			
			// v = s / t
			// s = v * t
			var currentPosition = entity.Position;
			
			var direction = targetPosition - currentPosition;
			if (direction.sqrMagnitude <= 2) {
				InvokeControllerFinished(entity);
				return;
			}
			
			var normalizedDirection = direction;
			normalizedDirection.Normalize();
			
			// need more speed for we do operate on a larger scale than in game 
			var offset = entity.Speed * Time.deltaTime * 10;
			var movementVector = normalizedDirection * offset;
			
			entity.Position = entity.Position + movementVector; 
		}		
	}
}

