using System;
using UnityEngine;
using Pux.Entities;

namespace Pux.Controllers
{
	public sealed class FloatController : EntityController
	{
		public FloatController(float seaLevel) {
			SeaLevel = seaLevel;
		}
		
		public float SeaLevel {
			get;
			set;
		}
		
		protected override void UpdateOverride (EntityBehaviour entity)
		{
			if (IsFinished || entity == null) {
				return;
			}
			
			var offset = Convert.ToSingle(Math.Sin(Time.timeSinceLevelLoad)) * 1.4f;
			var pos = entity.transform.position;
			 entity.transform.position = new Vector3(pos.x, SeaLevel + offset, pos.z);
		}
	}
}

