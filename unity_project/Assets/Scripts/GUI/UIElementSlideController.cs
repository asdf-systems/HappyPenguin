using System;
using UnityEngine;
using Pux.Unity2;

namespace Pux.UI
{
	public sealed class UIElementSlideController : UIElementController<GUIManager>
	{
		private Func<float, float> function;
		private TimeSpan elapsedTime = TimeSpan.Zero; 

		public UIElementSlideController()
			: this((x) => x) { }
		
		public UIElementSlideController(Func<float, float> function) {
			this.function = function;
		}
		
		public TimeSpan Duration {
			get;
			set;
		}
		
		public Vector2 StartPosition {
			get;
			set;
		}
		
		public Vector2 TargetPosition {
			get;
			set;
		}
		
		protected override void UpdateOverride (UIElementBehaviour<GUIManager> entity)
		{	
			if (IsFinished) {
				return;
			}
			
			elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
			
			if (elapsedTime >= Duration) {
				elapsedTime = TimeSpan.Zero;
				InvokeControllerFinished(entity);
				return;
			}
			
			var relTime = (float)elapsedTime.TotalMilliseconds / (float)Duration.TotalMilliseconds;
			if (relTime > 1.0f) {
				relTime = 1.0f;
			}
			var relDistance = function(relTime); 
			// need more speed for we do operate on a larger scale than in game 
			
			var vec = (TargetPosition - StartPosition);
			entity.Position = StartPosition + (vec * relDistance); 
		}		
	}
}

