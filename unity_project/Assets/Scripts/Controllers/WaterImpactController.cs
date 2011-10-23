using System;
using UnityEngine;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Controllers
{
	public sealed class WaterImpactController : EntityController
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
			var reverse = 1 - percent;
			
			// create impact wave form, with slight depth offset, since we wont start at 0.
			var sin = -Math.Sin(percent * Math.PI * 16 * reverse + Math.PI / 6);
			
			// reduce amplitude above the sea level, we can't fly
			var strength = sin < 0 ? Strength : Strength / 1.5;
			
			// reduce amplitude exponentially
			strength *= (float) reverse * (float) reverse;
			
			// add amplitude modulation to wave
			var offset = (float) (strength * sin);
			
			// apply offset to sealevel depth
			var p = entity.transform.position;
			entity.transform.position = new Vector3(p.x, SeaLevel + offset, p.z);
			elapsedTime += TimeSpan.FromSeconds(Time.deltaTime);
		}
		
		#endregion
	}
}

