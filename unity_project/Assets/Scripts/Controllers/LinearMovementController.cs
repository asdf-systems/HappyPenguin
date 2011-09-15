using System;
using UnityEngine;
using HappyPenguin.Entities;

namespace HappyPenguin.Controllers
{
	public sealed class LinearMovementController : Controller<EntityBehaviour>
	{
		private Vector3 target;
		private TimeSpan duration;
		
		public LinearMovementController(Vector3 target, TimeSpan duration) {
			this.target = target;
			this.duration = duration;
		}
		
		public override void Start()
		{
			
		}
		
		public override void Update(EntityBehaviour entity)
		{
			
		}
		
		public override void Stop()
		{
			
		}
		
	}
}

