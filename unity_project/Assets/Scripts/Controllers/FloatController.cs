using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public sealed class FloatController : Controller<EntityBehaviour>
	{
		public FloatController(float baseline) {
			Baseline = baseline;
		}
		
		public float Baseline {
			get;
			set;
		}
		
		public override void Update (EntityBehaviour entity)
		{
			var offset = Convert.ToSingle(Math.Sin(Time.timeSinceLevelLoad));
			var pos = entity.transform.position;
			 entity.transform.position = new Vector3(pos.x, Baseline + offset, pos.z);
		}
	}
}

