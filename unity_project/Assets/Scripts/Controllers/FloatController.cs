using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
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
			var offset = Convert.ToSingle(Math.Sin(Time.timeSinceLevelLoad)) * 2;
			var pos = entity.transform.position;
			 entity.transform.position = new Vector3(pos.x, SeaLevel + offset, pos.z);
		}
	}
}

