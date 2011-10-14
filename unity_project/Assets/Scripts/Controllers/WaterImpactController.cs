using System;
using UnityEngine;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Controllers
{
	public sealed class WaterImpactController : Controller
	{
		private TimeSpan elapsedTime;

		public WaterImpactController(float seaLevel) {
			Strength = 10;
			Duration = TimeSpan.FromSeconds(4);
			SeaLevel = seaLevel;
		}

		public float SeaLevel { get; set; }

		public TimeSpan Duration { get; set; }

		public float Strength { get; set; }

		#region implemented abstract members of HappyPenguin.Controllers.Controller[EntityBehaviour]
		protected override void UpdateOverride(EntityBehaviour entity) {
			var isCloseEnough = entity.transform.position.y - SeaLevel < 0.02f;
			if (elapsedTime >= Duration && isCloseEnough) {
				InvokeControllerFinished(entity);
				return;
			}
			
			var percent = elapsedTime.TotalMilliseconds / Duration.TotalMilliseconds;
			// 0.0f - 1.0f
			var reverse = 1 - percent;
			
			var offset = (float)-(reverse * (-Math.Sin(reverse * Duration.TotalSeconds * Math.PI)));
			offset = offset < 0 ? offset * Strength : offset * Strength / 2f;
			//Debug.Log("Offset: " + offset);
			
			var p = entity.transform.position;
			entity.transform.position = new Vector3(p.x, SeaLevel + offset, p.z);
			elapsedTime += TimeSpan.FromSeconds(Time.deltaTime);
		}
		
		#endregion
	}
}

