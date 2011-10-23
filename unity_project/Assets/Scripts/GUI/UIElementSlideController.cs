using System;
using UnityEngine;
using HappyPenguin.Unity2;

namespace HappyPenguin.UI
{
	public sealed class UIElementSlideController : UIElementController<GUIManager>
	{
		private readonly Vector2 targetPosition;
		private Func<float, float> function;
	
		private TimeSpan elapsedTime;

		public UIElementSlideController(Vector2 targetPosition)
			: this(targetPosition, (x) => x) { }
		
		public UIElementSlideController(Vector2 targetPosition, Func<float, float> function) {
			this.targetPosition = targetPosition;
			this.function = function;
		}
		
		protected override void UpdateOverride (UIElementBehaviour<GUIManager> entity)
		{
			var isCloseEnough = entity.Position.IsCloseEnoughTo(targetPosition);
			if (isCloseEnough) {
				InvokeControllerFinished(entity);
				return;
			}
			
			elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
			
			// v = s / t
			// s = v * t
			var currentPosition = entity.Position;
			
			var direction = targetPosition - currentPosition;
			
			var normalizedDirection = direction;
			normalizedDirection.Normalize();
			
			var offset = entity.Speed * Time.deltaTime;
			var movementVector = normalizedDirection * offset;
			
			entity.Position = entity.Position + movementVector; 
		}		
	}
}

